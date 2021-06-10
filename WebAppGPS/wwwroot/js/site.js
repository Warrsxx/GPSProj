// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//ctrlShowContainers("#divListPesq", false);
//ctrlShowContainers("#divResPesq", false);
//ctrlShowContainers("#divEmpCad", false);


$('#btnEmpresasCadastradas').click(function () {
    $('#EmpresasCadastradas').empty();
    $.ajax({
        dataType: "json",
        type: "GET",
        url: "/Empresa/GetEmpresas",
        success: function (data) {

            var element = "EmpresasCadastradas";
            console.log(data);
            CreateTableFromJSONEmpCad(data, element);

            ctrlShowContainers('#divEmpCad', true);
        }
    });
});





$('#btnAddList').click(function () {
    $('#ResultadoPesquisa').empty();



    var cnpj = $("#CNPJ").val();
    cnpj = cnpj.toString();

    

    var arr = [];

    var li = $("#ListaPesquisa ul li").toArray();
    var exist = false;



    if ($("#ListaPesquisa").has('li').length)
    {
        for (var i = 0; i < li.length; i++) {
            var lstcnpj = li[i].innerHTML;
            console.log(lstcnpj);

            if (lstcnpj == cnpj) {
                exist = true;
                    break;
                }
            }

        if (!exist) {
            arr.push(cnpj);
        } else {
            alert('Valor já inserido na lista!');
            return;
        }
        
    } else {
        arr.push(cnpj);
        $("#btnPesquisarList").prop("disabled", false);
    }

    arr.forEach(addListItem);

    ctrlShowContainers('#divListPesq', true);

});

function addListItem(cnpj) {
    $("#ListaPesquisa ul").append("<li>" + cnpj + "</li>");
}

$('#btnPesquisarList').click(function () {
    pesquisaList();
    ctrlShowContainers('#divResPesq', true);
});

function pesquisaList()
{
    $('#ResultadoPesquisa').empty();

    var li = $("#ListaPesquisa ul li").toArray();

    for (var i = 0; i < li.length; i++) {
        var lstcnpj = li[i].innerHTML;
        lstcnpj = lstcnpj.replace(/[^0-9]/g, '');
        pesquisaEmpresas(lstcnpj);
    }
}












$('#btnPesquisar').click(function () {
    var cnpj = $("#CNPJ").val();
    cnpj = cnpj.toString();
    cnpj = cnpj.replace(/[^0-9]/g, '');
    pesquisaEmpresas(cnpj);
    $('#ResultadoPesquisa').empty();
    ctrlShowContainers('#divResPesq', true); 

});





function pesquisaEmpresas(cnpj) {
    $.ajax({
        dataType: "json",
        type: "GET",
        url: "/Empresa/GetEmpresasWService",
        data: "cnpj=" + cnpj + "",
        success: function (data) {

            var element = "ResultadoPesquisa";
            console.log(data);
            CreateTableFromJSON(data, element);
        }, error: function () { alert("error !"); }
    });
}


$('#btnClosePesq').click(function () {

    //alert('chegou');    
    $('#lstCnpj').innerHTML=""
    ctrlShowContainers('#divListPesq', false);

    $('#lstCnpj').empty();
    $('#ResultadoPesquisa').empty();
    ctrlShowContainers('#divResPesq', false);

});
$('#btnCloseRes').click(function () {

    //alert('chegou');    
    ctrlShowContainers('#divResPesq', false);
    $('#ResultadoPesquisa').empty();


});
$('#btnCloseCad').click(function () {

    //alert('chegou');    
    ctrlShowContainers('#divEmpCad', false);
    $('#EmpresasCadastradas').empty();

});
 

function ctrlShowContainers(div, show)
{
    if (show) {
        $(div).show("fast");
    } else {
        $(div).hide("fast");
    }
}


function CreateTableFromJSON(data, element) {

    var divContainer = document.getElementById(element);


    // EXTRACT VALUE FOR HTML HEADER. 

 
    var col = [];
    

    for (var i = 0; i < data.length; i++) {
        for (var key in data[i]) {

            if (col.indexOf(key) === -1) {
                col.push(key);
            }
        }
    }

    // CREATE DYNAMIC TABLE.






    var table = document.createElement("table");
    table.setAttribute("id", "tblEmpresas");
    table.setAttribute("style", "white-space: nowrap;");

    // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

    var tr = table.insertRow(-1);                   // TABLE ROW.

    for (var i = 0; i < col.length; i++) {
        var th = document.createElement("th");      // TABLE HEADER.
        th.innerHTML = col[i];
        tr.appendChild(th);
    }

    // ADD JSON DATA TO THE TABLE AS ROWS.
    for (var i = 0; i < data.length; i++) {

        tr = table.insertRow(-1);


        for (var j = 0; j < col.length; j++) {
            var tabCell = tr.insertCell(-1);

            //console.log(data[i][col[j]]);

            var cell = data[i][col[j]];

            if (i == 0 && j == 0) {


                var divBtn = document.createElement("DIV");
                divBtn.classList.add("tooltipCustom");

                var btn = document.createElement("BUTTON");
                btn.classList.add("btn");
                btn.classList.add("btn-secondary");
                btn.innerHTML = "<i class='fas fa-save'></i>";
                btn.setAttribute("id", "btnSalvarEmpresa");


                var span = document.createElement("SPAN");
                span.classList.add("tooltiptextCustom");
                span.textContent = "Gravar";
                    ///setAttribute("text", "Gravar dados na base");

               

                //var dataEmp = JSON.parse(data);


                var dataEmp = JSON.parse(JSON.stringify(data))

                //var dataEmp = data;

                console.log(dataEmp);

                btn.onclick = function () {

                    SalvarEmpresa(dataEmp);
                };
 
                divBtn.appendChild(btn);
                divBtn.appendChild(span);

                tabCell.appendChild(divBtn);
            } else {
                tabCell.innerHTML = cell;
            }            

        }
    }

    // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.

    //divContainer.innerHTML = "";
    table.classList.add("table-bordered");
    table.classList.add("table-striped");
    table.classList.add("table-hover");
    //table.classList.add("table-secondary");

    divContainer.appendChild(table);
}



function SalvarEmpresa (data) {

    var dataEmp = JSON.parse(JSON.stringify(data));

    datastringify = JSON.stringify(dataEmp);

    console.log(datastringify);

    $.ajax({
        dataType: "json",
        type: "POST",
        url: "/Empresa/SaveEmpresas",
        data: { empresa : datastringify },
        success: function (data) {
            alert(data);
        }, error: function () { alert("error !"); }
    });
};
 


function CreateTableFromJSONEmpCad(data,element) {

    var divContainer = document.getElementById(element);
    // document.getElementById(element);

    // EXTRACT VALUE FOR HTML HEADER. 
    // ('Book ID', 'Book Name', 'Category' and 'Price')
    var col = [];
    for (var i = 0; i < data.length; i++) {
        for (var key in data[i]) {
            if (col.indexOf(key) === -1) {
                col.push(key);
            }
        }
    }

    // CREATE DYNAMIC TABLE.


    var table = document.createElement("table");
    table.setAttribute("id", "tblEmpresasCad");
    table.setAttribute("style", "white-space: nowrap;");

    // CREATE HTML TABLE HEADER ROW USING THE EXTRACTED HEADERS ABOVE.

    var tr = table.insertRow(-1);                   // TABLE ROW.

    for (var i = 0; i < col.length; i++) {
        var th = document.createElement("th");      // TABLE HEADER.
        th.innerHTML = col[i];
        tr.appendChild(th);
    }

    // ADD JSON DATA TO THE TABLE AS ROWS.
    for (var i = 0; i < data.length; i++) {

        tr = table.insertRow(-1);

        for (var j = 0; j < col.length; j++) {

            var tabCell = tr.insertCell(-1);

            tabCell.innerHTML = data[i][col[j]];

            //if (data[i][col[j]] != null) {
            //    if (data[i][col[j]].length > 0)
            //    {
            //        if (col[j] == "atividades_secundarias")
            //        {
            //            console.log(col[j]);
            //            console.log(data[i][col[j]]);
            //        }
            //    }
            //}
        }
    }

    // FINALLY ADD THE NEWLY CREATED TABLE WITH JSON DATA TO A CONTAINER.

    //divContainer.innerHTML = "";
    table.classList.add("table-bordered");
    table.classList.add("table-striped");
    table.classList.add("table-hover");
    //table.classList.add("table-secondary");
     
    divContainer.appendChild(table);
}



 