$(document).ready(function () {
    var idCondominio = $(`#idCondominio`);
    var idUnidade = $(`#IdUnidade`);
    var urlListarPorCondominio = "/Unidade/ListarPorCondominio";

    $(idCondominio).change(function () {

        if (idCondominio.value != "") {
            $.ajax({
                type: `GET`,
                url: urlListarPorCondominio + "?idCondominio=" + idCondominio.val(),
                dataType: `json`,
                success: function (data) {
                    idUnidade.prop(`disabled`, false);

                    idUnidade.html(``);
                    idUnidade.append(new Option(`Unidade`, ``));
                    $.each(data, function (index, item) {
                        idUnidade.append(new Option(item.text, item.value));
                    });
                }
            });
        }
        else {
            idUnidade.prop(`disabled`, true);
        }
    });
});