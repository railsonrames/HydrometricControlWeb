(function (root, window, document, undefined) {
    var leituraApp = root.leituraApp = {
        idCondominio: undefined,
        idUnidade: undefined,
        urlListarPorCondominio: undefined
    };

    var self = this;

    $(self.idCondominio).change(function () {       
        
        if (self.idCondominio.value != "") {
            $.ajax({
                type: `GET`,
                url: leituraApp.urlListarPorCondominio + "?idCondominio=" + self.idCondominio.value,
                dataType: `json`,                
                success: function (data) {
                    $(`#idUnidade`).prop(`disabled`, false);
                    
                    $(`#idUnidade`).html(``);
                    $(`#idUnidade`).append(new Option(`Unidade`,``));
                    $.each(data, function (index, item) {
                        $(`#idUnidade`).append(new Option(item.text, item.value));
                    });
                }
            });
        }
        else {
            $(`#idUnidade`).prop(`disabled`, true);
        }
    });

})(this.window, this.window, this.window.document);