/// <reference path="../../bootstrap.js" />
BMS.AppConstants = {
    SessionTokenKey: "sessionToken",
    MessageType: {
        Error: "Error",
        Info: "Info",
        Warning: "Warning",
        Success: "Success"
    },

    URL: {
        Action: {
            AccessDenied: BMS.AppVar.AppHost + "/Error/AccessDenied",
            Distributor: {
                ChangeDistributorStatus: BMS.AppVar.AppHost + "/Distributor/ChangeDistributorStatus",
            },
            Item: {
                ChangeItemStatus: BMS.AppVar.AppHost + "/Item/ChangeItemStatus",
            },
            Order: {
                AddItemToOrder: BMS.AppVar.AppHost + "/Order/AddItemToOrder",
                AddToOrderPartial: BMS.AppVar.AppHost + "/Order/AddToOrderPartial",
                DispatchItemInOrder: BMS.AppVar.AppHost + "/Order/DispatchItemInOrder"
            },
            Manufacturer: {
                ChangeManufacturerStatus: BMS.AppVar.AppHost + "/Manufacturer/ChangeManufacturerStatus",
            },

            Account: {
                SetPassword: BMS.AppVar.AppHost + "/Account/SetPassword",
                ChangeUserStatus: BMS.AppVar.AppHost + "/Account/ChangeUserStatus",
            },
            Common: {
                GetDistributorManufacturerList: BMS.AppVar.AppHost + "/Common/GetDistributorManufacturerList"
            },

        },
        API: {
            CandidateTestService: BMS.AppVar.AppHost + "/api/CandidateTestService"
        },
        Script: {
            //MathJax: "http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML",
            JQuery: BMS.AppVar.AppHost + "/Scripts/jquery-1.10.2.min.js",
            Bootstrap: BMS.AppVar.AppHost + "/Scripts/bootstrap.js",
            JqueryHotKeys: BMS.AppVar.AppHost + "/Scripts/jquery.hotkeys.min.js",
            Mousetrap: BMS.AppVar.AppHost + "/Scripts/mousetrap.min.js",
            MathJax: BMS.AppVar.AppHost + "/Scripts/MathJax/MathJax.js?config=TeX-AMS-MML_HTMLorMML",
            WirisEditor: "http://www.wiris.net/demo/editor/editor",
            Application: {
                Question: {
                    kendoEditorIframe: BMS.AppVar.AppHost + "/Scripts/Application/Question/QuestionEditorIframeManager.js"
                }
            }
        },
        Style: {
            kendoEditorIframeStyle: BMS.AppVar.AppHost + "/Content/kendoEditorIframeStyle.css",
        },
    },
    HttpStatusCode: {
        Ok: 'OK',
        Forbidden: 'Forbidden',
        InternalServerError: 'Internal Server Error'
    },
    HttpStatusCodeMessage: {
        Forbidden: 'Sorry! Access is denied. You have no permission to perform this action.'
    },
    ImagePath: {
        AjaxLoader: BMS.AppVar.AppHost + "/Content/images/ajax-loader.gif",
        NoImage: BMS.AppVar.AppHost + "/Content/images/noimage.png"
    },
    TimeIntervals: {

    },
    MouseCurrentPosition: {
        X: undefined,
        Y: undefined
    },
    ActiveStatus: {
        Active: "Active",
        InActive: "InActive"
    },
    EntityStatus: {
        All: -1,
        InActive: 0,
        Active: 1,
        Delete: 2
    },
    ActionType: {
        Add: "Add",
        Edit: "Edit",
        Delete: "Delete"
    },
    Commands: {
        Add: "Add",
        Edit: "Edit",
        Delete: "Delete",
        Question: "Question"
    },
    UserRole: {
        SuperAdmin: "superadmin",
        Admin: "admin",
        Student: "student",
        DataEntry: "dataentry",
        Teacher: "teacher",
    },
}