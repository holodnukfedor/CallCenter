function showErrorDialog(errorMsg) {
    $('#errorDialogMessage').html(errorMsg);
    $('#errorDialog').dialog("open");
}

function initializeDateTimePicker(elem)
{
    $(elem).datetimepicker({
        beforeShow: function() {
            setTimeout(function(){
                $('.ui-datepicker').css('z-index', 99999999999999);
            }, 0);
        },

        showSecond: true,
        timeFormat: 'HH:mm:ss',
        dateFormat: "dd.mm.yy"
    });
}

function initializePhoneCallGrid()
{
    $("#phoneCallTable").jqGrid({
        url: 'Home/GetPhoneCallsJsonList',
        datatype: "json",
        autowidth: true,
        height: 'auto',
        ajaxSelectOptions: { type: "GET" },
        colNames: ['Id', 'Продолжительность', 'Статус', 'Время начала', 'Время соединения', 'Время терминирования', 'Участники разговора', 'Id дочерних зв.', 'Id родительского зв.'],
        colModel: [
            {
                name: 'Id', index: 'Id', width: 40, sortable: true, editable: false,
                searchoptions: { sopt: ['gt', 'lt', 'ge', 'le', 'eq', 'ne'] },
                searchrules: { integer:true, minValue: 0, maxValue: 2147483647, required: true }
            },
            {
                name: 'Duration', index: 'Duration', width: 50, sortable: true, editable: false, search: true,
                searchoptions: { sopt: ['gt', 'lt', 'ge', 'le', 'eq', 'ne'] },
                searchrules: { integer: true, minValue: 0, maxValue: 2147483647, required: false }
            },
            {
                name: 'Status', index: 'Status', width: 40, sortable: true, editable: false, search: true,
                stype: "select",

                searchoptions: {
                    dataUrl: 'Home/GetPhoneStatusesDropdown',
                    defaultValue : '1',
                    sopt: ['eq'],
                },
                searchrules: { integer: true, minValue: 0, maxValue: 2147483647, required: true }
            },
            {
                name: 'StartTime', index: 'StartTime', width: 50, sortable: true, editable: false, search: true,
                searchoptions: {
                    sopt: ['gt', 'lt', 'ge', 'le', 'eq', 'ne'], dataInit: initializeDateTimePicker
                },
                searchrules: { required: true }
            },
            {
                name: 'ConnectionTime', index: 'ConnectionTime', width: 50, sortable: true, editable: false, search: true,
                searchoptions: { sopt: ['gt', 'lt', 'ge', 'le', 'eq', 'ne'], dataInit: initializeDateTimePicker },
                searchrules: { required: false }
            },
            {
                name: 'TerminationTime', index: 'TerminationTime', width: 50, sortable: true, editable: false, search: true,
                searchoptions: { sopt: ['gt', 'lt', 'ge', 'le', 'eq', 'ne'], dataInit: initializeDateTimePicker },
                searchrules: { required: true }
            },
            {
                name: 'UserInfo', index: 'UserInfo', width: 120, sortable: false, editable: false, search: true,
                searchoptions: { sopt: ['cn'] },
                searchrules: { required: true }
            },
            {
                name: 'ChildCallIds', index: 'ChildCallIds', width: 60, sortable: false, editable: false, search: true,
                searchoptions: { sopt: ['cn'] },
                searchrules: { integer: true, minValue: 0, maxValue: 2147483647, required: true }
            },
            {
                name: 'ParentCallId', index: 'ParentCallId', width: 30, sortable: true, editable: false, search: true,
                searchoptions: { sopt: ['gt', 'lt', 'ge', 'le', 'eq', 'ne'] },
                searchrules: { integer: true, minValue: 0, maxValue: 2147483647, required: false }
            }
        ],
        jsonReader: {
            repeatitems: false,
            root: "PhoneCallList",
            page: "PageNumber",
            total: "PagesCount",
            records: "RowsCount"
        },
        rowNum: 10,
        rowList: [10, 25, 50, 100],
        pager: '#phoneCallTablePager',
        viewrecords: true,
        sortorder: "asc",
        sortname: 'Id',
        caption: "Телефонные вызовы",
        sortable: true,
        loadError: function (xhr, st, err) {
            if (xhr.status == 500) {
                showErrorDialog('Загрузка списка товаров. Внутренняя ошибка сервера. ');
                $('#phoneCallTable').trigger('reloadGrid');
            }
            if (xhr.status == 0) {
                showErrorDialog('Загрузка списка товаров. Сервер не отвечает. ');
            }
        },
    });


    $("#phoneCallTable").jqGrid('navGrid', '#phoneCallTablePager', {
            search: true,
            refresh: true,
            add: false,
            del: false,
            edit: false,
            view: true,
            viewtext: "Подробно",
            viewicon: 'ui-icon-zoomin',
            refreshtext: 'Обновить',
            searchtext: 'Фильтр'
        },
            null,
            null,
            null,
            {
                width: 600,
                recreateForm: true,
                multipleSearch: true,
                caption: "Фильтр...",
                onSearch: function () {
                    $('select.input-elm').change();
                }
            },
            { width: 600, recreateForm: true }
    );
}

$(function () {
    $.ajaxSetup({ cache: false });

    $("#errorDialog").dialog({ autoOpen: false });

    initializePhoneCallGrid();
});