using Syncfusion.Data;
using Syncfusion.UI.Xaml.Controls.Input;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using Syncfusion.UI.Xaml.ScrollAxis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Update_Live_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            dataGrid.AllowEditing = true;
            dataGrid.LiveDataUpdateMode = LiveDataUpdateMode.AllowSummaryUpdate;
            this.dataGrid.CellRenderers.Remove("Numeric");
            this.dataGrid.CellRenderers.Add("Numeric", new CustomizedGridCellNumericRenderer(dataGrid));
        }
    }
    internal class CustomizedGridCellNumericRenderer : GridCellNumericRenderer
    {
        RowColumnIndex RowColumnIndex;
        SfDataGrid DataGrid { get; set; }
        string newvalue = null;

        public CustomizedGridCellNumericRenderer(SfDataGrid dataGrid)
        {
            DataGrid = dataGrid;
        }

        public override void OnInitializeEditElement(DataColumnBase dataColumn, SfNumericTextBox uiElement, object dataContext)
        {
            base.OnInitializeEditElement(dataColumn, uiElement, dataContext);
            uiElement.ValueChanged += UiElement_ValueChanged;
            this.RowColumnIndex.ColumnIndex = dataColumn.ColumnIndex;
            this.RowColumnIndex.RowIndex = dataColumn.RowIndex;
        }

        private void UiElement_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            newvalue = e.NewValue.ToString();
            UpdateSummaryValues(this.RowColumnIndex.RowIndex, this.RowColumnIndex.ColumnIndex);
        }

        private void UpdateSummaryValues(int rowIndex, int columnIndex)
        {
            string editEelementText = newvalue == "0" ? "0" : newvalue;
            columnIndex = this.DataGrid.ResolveToGridVisibleColumnIndex(columnIndex);
            if (columnIndex < 0)
                return;
            var mappingName = DataGrid.Columns[columnIndex].MappingName;
            var recordIndex = this.DataGrid.ResolveToRecordIndex(rowIndex);
            if (recordIndex < 0)
                return;
            if (DataGrid.View.TopLevelGroup != null)
            {
                var record = DataGrid.View.TopLevelGroup.DisplayElements[recordIndex];
                if (!record.IsRecords)
                    return;
                var data = (record as RecordEntry).Data;
                data.GetType().GetProperty(mappingName).SetValue(data, (int.Parse(editEelementText)));
            }
            else
            {
                var record1 = DataGrid.View.Records.GetItemAt(recordIndex);
                record1.GetType().GetProperty(mappingName).SetValue(record1, (int.Parse(editEelementText)));
            }
        }
    }
}
