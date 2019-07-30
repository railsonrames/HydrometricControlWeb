$(document).ready(function () {
    $('#botaoDeUpload').on('click', function () {
        var extensaoDoArquivo = ['xls', 'xlsx']
        var nomeDoArquivo = $('#arquivoParaSubir').val();
        if (nomeDoArquivo.length == 0) {
            alert("Selecione um arquivo, meu jovem.");
            return false;
        }
        else {
            var extensaoSeparada = nomeDoArquivo.replace(/^.*\./, '');
            if ($.inArray(extensaoSeparada, extensaoDoArquivo) == -1) {
                alert("Selecione um arquivo Excel, garotinho.");
                return false;
            }
        }
        var fdata = new FormData();
        var arquivoParaSubir = $('#arquivoParaSubir').get(0);
        debugger;
        var arquivos = arquivoParaSubir.files;
        fdata.append(arquivos[0].name, arquivos[0]);
        $.ajax({
            type: "POST",
            url: "/Leitura/OnPostImportar",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (response) {
                if (response.length == 0) {
                    alert('Alguma treta detectada na subida do arquivo.');
                    console.log('Deu ruim!');
                }
                else {
                    console.log('Estranho, mas chegou onde devia!');
                    debugger;
                    $(`#dvData`).html(response);
                }
            },
            error: function (excecao) {
                console.log('Deu ruim 2!');
                $(`#dvData`).html(excecao.responseText);
            }
        });
    });
});
