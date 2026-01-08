# How to Update the Table Summary Value When the Cell in Edit Mode in UWP DataGrid?

This sample illustrates how to update the table summary value when the cell in edit mode in [UWP DataGrid](https://www.syncfusion.com/uwp-ui-controls/datagrid) (SfDataGrid).

In `DataGrid`, you can update the summary values when you are changing the values by overriding `OnInitializeEditElement` method and `UiElement.ValueChanged` event in [GridNumericCelllRenderer](https://help.syncfusion.com/cr/uwp/Syncfusion.UI.Xaml.Grid.Cells.GridCellNumericRenderer.html).

#### C#
```c#
dataGrid.AllowEditing = true;
dataGrid.LiveDataUpdateMode = LiveDataUpdateMode.AllowSummaryUpdate;

this.dataGrid.CellRenderers.Remove("Numeric");
this.dataGrid.CellRenderers.Add("Numeric", new CustomizedGridCellNumericRenderer(dataGrid));

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
```

![DataGrid displays the updated table summary upon cell editing](TableSummaryUpdate.gif)

## Requirements to run the demo
 Visual Studio 2015 and above versions
