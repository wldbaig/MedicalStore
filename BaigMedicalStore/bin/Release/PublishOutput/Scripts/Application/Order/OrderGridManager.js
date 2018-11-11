var orderGridManager = new OrderGridManager();

$(function () {
    orderGridManager.Init();
});


function OrderGridManager() {

    var that = this;

    var globalVar = {

    };

    var domElement = {}

    this.Init = function () {
        InitializeVariables();
        Initialization();
        BindEvents();
    }

    function InitializeVariables() {
        domElement = {
            OrderGrid: $("#OrderGrid"),
            date: $("#Date"),
           
            ddlDistributor: $('#Distributor'),
            btnSearch: $('#btnSearch'),
            btnReset: $('#btnReset'),
            SearchPanelInput: $('.search-panel-input input[type=text]'),

            SetSearchCriteria: $('.search-panel-input')
        }
    }


    function Initialization() {
        LoadGridState();
    }

    function BindEvents() {

        domElement.btnSearch.off();
        domElement.btnSearch.on('click', function () {
            SearchGrid();
        });

        domElement.btnReset.off();
        domElement.btnReset.on('click', function () {
            ResetGrid();
        });

        domElement.SearchPanelInput.off();
        domElement.SearchPanelInput.on('keyup', function (event) {
            if (event.keyCode == 13) {
                SearchGrid();
            }

        });
    }

    function SearchGrid() {

        debugger;

        var filters = [];
        var grid = domElement.OrderGrid.data("kendoGrid");

        var Date = domElement.date.data("kendoDatePicker").value();
         
        var Distributor = domElement.ddlDistributor.val();

       
        if (Date != "") {
            filters.push({ field: "Date", operator: "eq", value: Date });
        }
         
        if (Distributor != "") {
            filters.push({ field: "Distributor", operator: "eq", value: Distributor });
        }

        

        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });

        //e.preventDefault();
        localStorage["kendo-Ordergrid-options"] = kendo.stringify(grid.getOptions());
    }

    function ResetGrid() {

        debugger;

        var filters = [];

        domElement.date.data("kendoDatePicker").value('');
         
        domElement.ddlDistributor.val("").data("kendoDropDownList").text("");  

        var grid = domElement.OrderGrid.data("kendoGrid");


        grid.dataSource.filter({
            logic: "and",
            filters: filters
        });
        localStorage.removeItem("kendo-Ordergrid-options");
    }


    function LoadGridState() {

        var grid = domElement.OrderGrid.data("kendoGrid");
        var gridDatasourceUrl = grid.dataSource.options.transport.read.url;
        var options = localStorage["kendo-Ordergrid-options"];

        if (options) {
            var search = JSON.parse(options);
            var searchDatasourceUrl = search.dataSource.transport.read.url;

            if (gridDatasourceUrl == searchDatasourceUrl) {
                var objFilters = search.dataSource.filter;
                var objAppliedfilters = objFilters.filters;

                objAppliedfilters.forEach(function (obj) {
                    //    alert(obj.field + ' - ' + obj.value);

                    if (fieldName == obj.field || fieldName == undefined) {
                        var element = domElement.SetSearchCriteria.find('[data-field=' + obj.field + ']');
                        if ($(element).data("kendoDropDownList") != undefined) {

                            $(element).data("kendoDropDownList").value(obj.value);
                            $(element).data("kendoDropDownList").trigger("change");

                        } else if ($(element).data("kendoDatePicker") != undefined) {

                            $(element).data("kendoDatePicker").value(obj.value);

                        } else if ($(element).data("kendoMultiSelect") != undefined) {
                            if (obj.value != undefined) {

                                var arr = obj.value.split(",");
                                $(element).data("kendoMultiSelect").value(arr);
                                $(element).data("kendoMultiSelect").trigger("change");
                            }
                        } else {
                            $(element).val(obj.value);
                        }
                    }
                });

                grid.setOptions(JSON.parse(options));
            }
        }
    }

    
}