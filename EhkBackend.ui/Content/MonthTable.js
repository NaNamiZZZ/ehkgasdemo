/**
 * Created by Administrator on 2017/11/8.
 */
// 加载combox
$("#combox").combo({
    width: '16%',
    height: 26,
    multiple: true

})
$("#sp").appendTo($("#combox").combo('panel'));
$("#sp input").click(function () {
    var text = $(this).next('span').text();
    var val = $(this).val();
    $("#combox").combo('setText', text).combo('setValue', val).combo('hidePanel');

})

// 加载部门下拉框
$("#part").combotree({
    url: 'json/userTree.json',
    height: 26,
    width: '16%',
    onSelect: function () {
        var t = $("#part").combotree('tree');
        var n = t.tree('getSelected');
        var text = n.text;
        $("#part").combotree('setValue', text);


    }


})
// 加载详情页面部门下拉框
$("#part01").combotree({
    url: 'json/userTree.json',
    height: 26,
    width: '70%',
    onSelect: function () {
        var t = $("#part01").combotree('tree');
        var n = t.tree('getSelected');
        var text = n.text;
        $("#part01").combotree('setValue', text);


    }


})
obj = {
    // 查询
    find: function () {
        $("#table").datagrid('load', {
            //user: $("#user").val(),
            //date: $.trim($("#dd").val()）

            account: $.trim($("#account_id").val()),
            email: $.trim($("#Email").val())
        })

    },
    // 添加
    addBox: function () {
        $("#addBox").dialog({
            closed: false

        });
        $("#name").val('');
        $("#pass").val('');
        $("#part01").combotree('setValue', '');
        var date = new Date();
        var y = date.getFullYear();
        var m = date.getMonth() + 1;
        var d = date.getDate();
        var str = y + '-' + m + '-' + d
        $("#time").datebox('setValue', str);


    },
    // 编辑
    edit: function (id) {
        var ID;
        $("#addBox").dialog({
            closed: false
        })
        $.ajax({
            url: '/Month1/GetOneMonth',
            type: 'post',
            dataType: 'json',
            data: {
                Mid_: id
            },
            success: function (res) {
                if (res) {
                    var data = res.Account;
                    //$.each(data, function (index) {
                    Account = res.Account;
                    if (id == Account) {
                        $('#addForm').form('load', {
                            //id: id,
                            //name: data[index].name,
                            //pass: data[index].pass,
                            //time: data[index].time
                            Account: res.Account,
                            Email: res.Email

                        })
                        //$("#part01").combotree('setValue', data[index].part);
                    }

                    //})




                }

            },
            error: function () {
                $.messager.show({
                    title: '提示',
                    msg: '更新失败'

                })

            }
        })

    },
    look: function () {
        $("#lookTail").dialog({
            closed: false

        })

    },
    sum: function () {
        $("#addForm").form('submit', {
            url: "/Month1/AddMonth",
            onSubmit: function () {
                return $(this).form('validate')

            },
            success: function (data) {
                var Account = $("#Account").val();
                var Email = $("#Email").val();
                //var time = $("#time").datebox('getValue');
                //var part = $("#part01").combotree('getValue');
                //var id = parseInt(Math.random() * 100000);
                $("#table").datagrid('insertRow', {
                    index: 1,
                    row: {
                        account: Account,
                        email: Email
                        //id: id,
                        //name: name,
                        //pass: pass,
                        //time: time,
                        //part: part
                    }


                })
                $("#addBox").dialog({
                    closed: true

                })
                $.messager.show({
                    title: '提示',
                    msg: '信息保存成功'
                })



            },
            error_emailnull: function (data) {
                $.messager.show({
                    title: '提示',
                    msg: 'email為空'
                })

            },
            error_accountnull: function (data) {
                $.messager.show({
                    title: '提示',
                    msg: 'Account為空'
                })
            },
            success_update: function (data) {
                $("#table").datagrid('insertRow', {
                    index: 1,
                    row: {
                        account: Account,
                        email: Email
                        //id: id,
                        //name: name,
                        //pass: pass,
                        //time: time,
                        //part: part
                    }


                })
                $("#addBox").dialog({
                    closed: true

                })
                $.messager.show({
                    title: '提示',
                    msg: '成功修改'
                })


            }

        })

    },
    // 删除多个
    del: function () {
        var rows = $("#table").datagrid("getSelections");
        if (rows.length > 0) {
            $.messager.confirm('确定删除', '你确定要删除你选择的记录吗？', function (flg) {
                if (flg) {
                    var ids = [];
                    for (i = 0; i < rows.length; i++) {
                        ids.push(rows[i].id);

                    }
                    var num = ids.length;
                    $.ajax({
                        type: 'post',
                        url: "",
                        data: {
                            ids: ids.join(',')
                        },
                        beforesend: function () {
                            $("#table").datagrid('loading');

                        },
                        success: function (data) {
                            if (data) {

                                $("#table").datagrid('loaded');
                                $("#table").datagrid('load');
                                $("#table").datagrid('unselectAll');
                                $.messager.show({
                                    title: '提示',
                                    msg: num + '个用户被删除'
                                })

                            }
                            else {
                                $.messager.show({
                                    title: '警示信息',
                                    msg: "信息删除失败"
                                })

                            }

                        }
                    })
                }

            })

        }
        else {
            $.messager.alert('提示', '请选择要删除的记录', 'info');
        }

    },

    //删除一个
    delOne: function (id) {
        //id = $("#table").datagrid('getSelected').;
        $.messager.confirm('提示信息', '是否删除所选择记录', function (flg) {
            if (flg) {
                $.ajax({
                    type: 'post',
                    url: '/Acemail/delAcemail',
                    data: {
                        Account_: id
                    },
                    beforesend: function () {
                        $("#table").datagrid('loading');

                    },
                    success: function (data) {
                        if (data) {
                            $("#table").datagrid("loaded");
                            $("#table").datagrid("load");
                            $("#table").datagrid("unselectRow");
                            $.messager.show({
                                title: '提示信息',
                                msg: "信息删除成功"
                            })
                        }
                        else {
                            $.messager.show({
                                title: '警示信息',
                                msg: "信息删除失败"
                            })

                        }

                    }
                })

            }

        })


    }
}
// 加载表格
initable();
function initable() {
    $("#table").datagrid({
        title: "電郵列表",
        iconCls: "icon-left02",
        url: '/Month1/GetMonth1List',
        fitColumns: true,
        striped: true,
        pagination: true,
        pageSize: 10,
        width: '100%',
        rownumbers: true,
        pageList: [10, 20],
        pageNumber: 1,
        nowrap: true,
        height: 'auto',
        sortName: 'id',
        checkOnSelect: false,
        sortOrder: 'asc',
        toolbar: '#tabelBut',
        columns: [[
                //{
                //    checkbox: true,
                //    field: 'billno',
                //    width: 10,
                //    align: 'center'
                //},
                {


                    field: 'mid',
                    title: 'Mid',
                    width: 20,
                    align: 'center'

                },
                {
                    field: 'bildate',
                    title: 'Billdate',
                    width: 20,
                    align: 'center',
                    formatter: function (value, row, index) {

                        if (row.bildate != null) {
                            //转换datetime类型
                            var da = new Date(parseInt((row.bildate).replace("/Date(", "").replace(")/", "").split("+")[0]));

                            return da.getFullYear() + "-" + (da.getMonth() + 1) + "-" + da.getDate();
                        } else {
                            return "  ";
                        }
                    }



                },
                {
                    field: 'Duedate',
                    title: 'Duedate',
                    width: 20,
                    align: 'center',
                    formatter: function (value, row, index) {

                        if (row.Duedate != null) {
                            //转换datetime类型
                            var da = new Date(parseInt((row.Duedate).replace("/Date(", "").replace(")/", "").split("+")[0]));

                            return da.getFullYear() + "-" + (da.getMonth() + 1) + "-" + da.getDate();
                        } else {
                            return "  ";
                        }
                    }



                },
                {
                
                
                    field: 'Rate',
                    title: 'Rate',
                    width: 20,
                    align:'center'
                
                },

                {
                   
                    field:'Remarks1',
                    title:'Remarks1',
                    width:20,
                    align:'center'
                
                },
                {
                    field:'Remarks2',
                    title:'Remarks2',
                    width:20,
                    align:'center'
                },
                //{
                //    field: 'time',
                //    title: '时间',
                //    width: 100,
                //    align: 'center'



                //}, {
                //    field: 'part',
                //    title: '部门',
                //    width: 100,
                //    align: 'center'



                //},
                //{
                //    field: 'title',
                //    title: '角色',
                //    width: 100,
                //    align: 'center'



                //},
                {
                    field: "opr",
                    title: '操作',
                    width: 100,
                    align: 'center',
                    formatter: function (val, row) {
                        e = '<a  id="add" data-id="98" class=" operA"  onclick="obj.edit(\'' + row.mid + '\')">编辑</a> ';
                        //c = '<a  id="look"   onclick="obj.look(\'' + row.id + '\')">流程查看</a> ';
                        //d = '<a  id="add" data-id="98" class=" operA01"  onclick="obj.delOne(\'' + row.bildate + '\')">删除</a> ';
                        return e ;

                    }

                }
        ]]
    })
}
// 弹出框加载
$("#addBox").dialog({
    title: "信息内容",
    width: 500,
    height: 300,
    closed: true,
    modal: true,
    shadow: true
})
// 加载物流详情
$("#lookTail").dialog({
    title: "信息内容",
    width: 650,
    height: 410,
    closed: true,
    modal: true,
    shadow: true
})