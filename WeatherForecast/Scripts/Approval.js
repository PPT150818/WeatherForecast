var selectedKeys;
var entity;
function ApprovalAccountGridRowChanged(s, e) {
    selectedKeys = null;
    selectedKeys = getSelectedRowKeys(s);
}

var tenantId;
function btnAccountApproveClick() {
    if (selectedKeys == null)
        ShowPopupMessage("Please select an Account to Approve", "Approve Account", 1);
    else {
        entity = "Account";
        AjaxRequest("POST", "api/APIApproval", "Approve", "", {
            "Entity": "1",
            "TenantId": tenantId,
            "SelectedIds": selectedKeys,
            "ApproveStatus": 2
        }, false, OnSaveApprovalSuccess, AjaxRequestError);
    }

}

function btnHouseholdApproveClick() {
    if (selectedKeys == null)
        ShowPopupMessage("Please select a Household to Approve", "Approve Household", 1);
    else {
        entity = "Household";
        AjaxRequest("POST", "api/APIApproval", "Approve", "", {
            "Entity": "2",
            "TenantId": tenantId,
            "SelectedIds": selectedKeys,
            "ApproveStatus": 2
        }, false, OnSaveApprovalSuccess, AjaxRequestError);
    }

}
function btnAccountRejectClick() {

    if (selectedKeys == null)
        ShowPopupMessage("Please select an Account to Reject", "Reject Account", 1);
    else {
        entity = "Account";
        btnRejectClick(selectedKeys, entity)
    }

}

function btnHouseholdRejectClick() {
    if (selectedKeys == null)
        ShowPopupMessage("Please select Household to reject", "Reject Household", 1);
    else {
        entity = "Household";
        btnRejectClick(selectedKeys, entity)
    }

}
function OnRejectSuccess(response) {
    try {
        if (response != null) {

            if (selectedKeys.toString().split(',').length == 1) {
                msg = "Selected " + entity;
            }
            else {
                msg = "Selected " + selectedKeys.toString().split(',').length + " " + entity + "s";
            }
            ShowPopupMessage(msg + " rejected successfully.", "Reject " + entity);

            var AccountGridInstance = $("#DashAccountsGrid").dxDataGrid("instance");
            if (AccountGridInstance != null) {
                AccountGridInstance.refresh();
            }

            var HHGridInstance = $("#DashHouseholdsGrid").dxDataGrid("instance");
            if (HHGridInstance != null) {
                HHGridInstance.refresh();
            }

        }
    }
    catch (err) {
        HandleError(err);
    }
}

function OnSaveApprovalSuccess(response) {
    if (response) {

        if (selectedKeys.toString().split(',').length == 1) {
            msg = "Selected " + entity;
        }
        else {
            msg = "Selected " + selectedKeys.toString().split(',').length + " " + entity + "s";
        }
        ShowPopupMessage(msg + " approved successfully.", "Approve " + entity);

        var AccountGridInstance = $("#DashAccountsGrid").dxDataGrid("instance");
        if (AccountGridInstance != null) {
            AccountGridInstance.refresh();
        }

        var HHGridInstance = $("#DashHouseholdsGrid").dxDataGrid("instance");
        if (HHGridInstance != null) {
            HHGridInstance.refresh();
        }
    }
}

//-------------Household_Start-----------------------------------
function ApprovalHouseholdGridRowChanged(s, e) {
    selectedKeys = null;
    selectedKeys = getSelectedRowKeys(s);
}

function btnHHApproveClick() {
    if (selectedKeys == null)
        ShowPopupMessage("Please select a Household to Approve", "Approve Household", 1);
    else {
        entity = "Household";
        AjaxRequest("POST", "api/APIApproval", "Approve", "", {
            "Entity": "2",
            "TenantId": tenantId,
            "SelectedIds": selectedKeys,
            "ApproveStatus": 2
        }, false, OnSaveApprovalSuccess, AjaxRequestError);
    }

}

function btnHHRejectClick() {    
    if (selectedKeys == null)
        ShowPopupMessage("Please select a Household to Reject", "Reject Account", 1);
    else {
        btnRejectClick(selectedKeys, "Household")
    }

}
//-------------Household_End-----------------------------------
function ApprovalAccountGridToolbarPreparing(e) {
    try {
        var toolbarItems = e.toolbarOptions.items;

        var rejectBtn = {
            widget: 'dxButton',
            options: {
                icon: "/Content/Images/reject.png", onClick: btnAccountRejectClick, hint: 'Reject'
            },
            location: 'after'
        }
        if (toolbarItems != null)
            toolbarItems.splice(0, 0, "", rejectBtn);

        var approveBtn = {
            widget: 'dxButton',
            options: {
                icon: "/Content/Images/Approve-24.png", onClick: btnAccountApproveClick, hint: 'Approve'
            },
            location: 'after'
        }

        if (toolbarItems != null)
            toolbarItems.splice(0, 0, "", approveBtn);

    } catch (err) {
        HandleError(err);
    }
}

function ApprovalHouseholdGridToolbarPreparing(e) {
    try {
        var toolbarItems = e.toolbarOptions.items;

        var rejectBtn = {
            widget: 'dxButton',
            options: {
                icon: "/Content/Images/reject.png", onClick: btnHouseholdRejectClick, hint: 'Reject'
            },
            location: 'after'
        }
        if (toolbarItems != null)
            toolbarItems.splice(0, 0, "", rejectBtn);

        var approveBtn = {
            widget: 'dxButton',
            options: {
                icon: "/Content/Images/Approve-24.png", onClick: btnHouseholdApproveClick, hint: 'Approve'
            },
            location: 'after'
        }

        if (toolbarItems != null)
            toolbarItems.splice(0, 0, "", approveBtn);

    } catch (err) {
        HandleError(err);
    }
}

function HouseholdsMarkForApproval_OnClick(approvalStatusId) {
    window.location.href = "/WSHousehold/Index?approvalStatus=" + approvalStatusId;
}

function AccountsMarkForApproval_OnClick(approvalStatusId) {
    window.location.href = "/WSAccountsCommon/Index?approvalStatus=" + approvalStatusId;
}

var enitityId;
var selectedAccKeys
function btnMarkForApprovalClick(selectedKeys, entity) {
    stringEntity = entity;
    selectedAccKeys = selectedKeys;
    AjaxRequest("POST", "api/APIApproval", "MarkForApproval", "", {
        "Entity": entity == "Account" ? "1" : "2",
        "SelectedIds": selectedKeys,
        "ApproveStatus": 1
    }, false, OnMarkForApprovalSuccess, AjaxRequestError);
}

function OnMarkForApprovalSuccess(response) {
    if (response) {
        if (selectedAccKeys.toString().split(',').length == 1) {
            msg = "Selected " + stringEntity;
        }
        else if (selectedAccKeys.toString().split(',').length > 1)
            msg = "Selected " + selectedAccKeys.toString().split(',').length + " " + stringEntity + "s";

        ShowPopupMessage(msg + " mark for approval successfully.", "Approve " + stringEntity, 1);

        AccountGridInstance = $("#AccountGrid").dxDataGrid("instance");
        if (AccountGridInstance != null || AccountGridInstance != undefined) {
            AccountGridInstance.deselectRows(selectedAccKeys);// 
            AccountGridInstance.refresh();
        }

        HHGridInstance = $("#dvHouseholdGrid").dxDataGrid("instance");
        if (HHGridInstance != null || HHGridInstance != undefined) {
            HHGridInstance.deselectRows(selectedAccKeys);// 
            HHGridInstance.refresh();
        }

    }
}

var stringEntity;
function btnApproveClick(selectedKeys, entity) {
    stringEntity = entity;
    AjaxRequest("POST", "api/APIApproval", "Approve", "", {
        "Entity": entity == "Account" ? "1" : "2",
        "SelectedIds": selectedKeys,
        "ApproveStatus": 2
    }, false, OnSaveApproveSuccess, AjaxRequestError);
}

function OnSaveApproveSuccess(response) {
    if (response) {
        if (selectedKeys.toString().split(',').length == 1) {
            msg = "Selected " + stringEntity;
        }
        else if (selectedKeys.toString().split(',').length > 1)
            msg = "Selected " + selectedKeys.split(',').length + " " + stringEntity + "s";

        ShowPopupMessage(msg + " approved successfully.", "Approve " + stringEntity, 1);

        var AccountGridInstance = $("#AccountGrid").dxDataGrid("instance");
        if (AccountGridInstance != null) {
            AccountGridInstance.refresh();
        }

        var HHGridInstance = $("#dvHouseholdGrid").dxDataGrid("instance");
        if (HHGridInstance != null) {
            HHGridInstance.refresh();
        }

        var dashAccountGridInstance = $("#DashAccountsGrid").dxDataGrid("instance");
        if (dashAccountGridInstance != null) {
            dashAccountGridInstance.refresh();
        }

        var dashHHGridInstance = $("#DashHouseholdsGrid").dxDataGrid("instance");
        if (dashHHGridInstance != null) {
            dashHHGridInstance.refresh();
        }
    }
}

var selectedIds;
var flag_screen;
function btnRejectClick(selectedKeys, entity) {

    selectedIds = selectedKeys;
    flag_screen = entity;
    AjaxRequest("GET", "Approval", "RejectApprovalAccount", "", {}, false, OnRejectSuccessTest, AjaxRequestError);
    //AjaxRequest("GET", "Approval", "RejectApprovalAccount", "", { selectedKeyVal: selectedKeys, screenName: entity }, false, OnRejectSuccessTest, AjaxRequestError);
}

function OnRejectSuccessTest(response) {
    try {
        if (response != null) {
            $("#commonPopupContiner").html(response);
        }
    }
    catch (err) {
        HandleError(err);
    }
}

function RejectApprovalButton(s, e) {

    var note = $("#RejectComment").dxTextArea("instance").option("value");

    AjaxRequest("POST", "api/APIApproval", "Approve", "", {
        "Entity": flag_screen == "Account" ? "1" : "2",
        "SelectedIds": selectedIds,
        "ApproveStatus": 3,
        "Note": note
    }, false, OnSaveRejectSuccess, AjaxRequestError);
}

function CancelRejectButton() {

    $("#rejectApprovalPopup").dxPopup("instance").option("visible", false);
}

function OnSaveRejectSuccess(response) {
    if (response) {
        $("#rejectApprovalPopup").dxPopup("instance").option("visible", false);
        if (selectedIds.toString().split(',').length == 1) {
            msg = "Selected " + flag_screen;
        }
        else if (selectedIds.toString().split(',').length > 1)
            msg = "Selected " + selectedIds.toString().split(',').length + " " + flag_screen + "s";

        ShowPopupMessage(msg + " rejected successfully.", "Reject " + flag_screen, 1);

        var AccountGridInstance = $("#AccountGrid").dxDataGrid("instance");
        if (AccountGridInstance != null) {
            AccountGridInstance.refresh();
        }

        var HHGridInstance = $("#dvHouseholdGrid").dxDataGrid("instance");
        if (HHGridInstance != null) {
            HHGridInstance.refresh();
        }

        var dashAccountGridInstance = $("#DashAccountsGrid").dxDataGrid("instance");
        if (dashAccountGridInstance != null) {
            dashAccountGridInstance.refresh();
        }

        var dashHHGridInstance = $("#DashHouseholdsGrid").dxDataGrid("instance");
        if (dashHHGridInstance != null) {
            dashHHGridInstance.refresh();
        }
    }
}

var approvedStatusId = 0;
function OnApproveStatusValueChanged(selectedItem) {
    approvedStatusId = selectedItem.selectedItem.Value;
    setTimeout(function () {
        var accountGrid = $("#AccountGrid").dxDataGrid("instance");
        if (accountGrid != undefined) {
            accountGrid.beginUpdate();
            accountGrid.columnOption('ApprovalStateName', 'visible', true);
            accountGrid.columnOption('Note', 'visible', true);
            accountGrid.refresh();
            accountGrid.endUpdate();
        }

        var HHGridInstance = $("#dvHouseholdGrid").dxDataGrid("instance");
        if (HHGridInstance != null) {
            HHGridInstance.beginUpdate();
            HHGridInstance.columnOption('ApprovalStateName', 'visible', true);
            HHGridInstance.columnOption('Note', 'visible', true);
            HHGridInstance.refresh();
            HHGridInstance.endUpdate();
        }
    }, 500);
}