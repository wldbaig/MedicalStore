var itemFormManager = new ItemFormManager();

$(function () {

    itemFormManager.Init();
});

function ItemFormManager() {

    var that = this;

    var globalVar = {

    };

    var domElement = {}

    this.Init = function () {
        InitializeVariables();
        Initialization();
    }

    function InitializeVariables() {

        domElement = {

            btnSubmitForm: $('#btnSubmitForm'),

            Name: $('#Name'),
            Formula: $('#Formula'),
            ddlLocation: $('#LocationId'),
            ddlCategory: $('#CategoryId'),
            ddlDistributor: $('#DistributorId'),
            ddlManufacturer: $('#ManufacturerId'),
            PiecesInPaking: $('#PiecesInPaking'),
            PurchasePrice: $('#PurchasePrice'),
            SalePrice: $('#SalePrice'),
            hfItemId: $('#hfItemId')
        }
    }

    function Initialization() {
        debugger;

    }

    function ResetForm() {
        domElement.Name.val("");

        domElement.Formula.val("");

        domElement.ddlManufacturer.data("kendoDropDownList").select(0);
        domElement.PiecesInPaking.val("");
        domElement.PurchasePrice.val(0); 
        domElement.SalePrice.val(0);
    }

    this.OnAjaxResponse = function (xhr, status, entityName) {
        debugger;
        var respnse = JSON.parse(xhr.responseText);
        var programFormViewModel = respnse.viewModel;
        var messageModel = respnse.messageModel;

        if (messageModel.Type == BMS.AppConstants.MessageType.Success) {
            ResetForm();
        }

        BMS.SiteScript.ShowMessage(messageModel);
        domElement.btnSubmitForm.attr('disabled', false);
    }


    this.DisableSubmitButton = function (xhr, status, entityName) {
        domElement.btnSubmitForm.attr('disabled', true);
    }

    this.DistributorList_onSelect = function (e) {
        UpdateManufacturer(this, e);
    }

    this.DistributorList_onDataBound = function (e) {
        UpdateManufacturer(this, e);
    }

    this.DistributorList_onChange = function (e) {
        UpdateManufacturer(this, e);
    }

    function UpdateManufacturer(object, e) {
        var dataItem = object.dataItem(e.item);
        var distribId = dataItem.Value;
        if (distribId != "") {
            GetManufactByDistrib(distribId); 
        }
    }

    function GetManufactByDistrib(distribId) {

        debugger;

        var itemId = domElement.hfItemId.val();

        var actionUrl = BMS.AppConstants.URL.Action.Common.GetDistributorManufacturerList + "?distribId=" + distribId + "&itemId=" + itemId;
        ServiceManager.Get(actionUrl, true, GetManufactByDistribCallBack, distribId);

    }

    function GetManufactByDistribCallBack(response, param) {
        debugger;
        if (response && response.length > 0 && response[0]) {
            debugger;
 
            var ddlManufact = domElement.ddlManufacturer.data("kendoDropDownList");
            ddlManufact.dataSource.data(response[1]);

            var allSelectEdSubjectIds = $.map(response[1], function (selectListItem) {
                if (selectListItem.Selected == true) {
                    return selectListItem.Value;
                }
            });

            domElement.ddlManufacturer.data("kendoDropDownList").value(allSelectEdSubjectIds);

         }
    }

}