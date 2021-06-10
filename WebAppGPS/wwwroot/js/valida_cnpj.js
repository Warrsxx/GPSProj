document.addEventListener('keydown', function (event) { //pega o evento de precionar uma tecla
     
    if (event.keyCode != 46 && event.keyCode != 8 ) {//verifica se a tecla precionada nao e um backspace e delete
        var i = document.getElementById("CNPJ").value.length; //aqui pega o tamanho do input
        if (i === 2 || i === 6 ) 
            document.getElementById("CNPJ").value = document.getElementById("CNPJ").value + ".";
        else if (i === 10) 
            document.getElementById("CNPJ").value = document.getElementById("CNPJ").value + "/";
        else if (i === 15) 
            document.getElementById("CNPJ").value = document.getElementById("CNPJ").value + "-";
    }
 

    document.getElementById("CNPJ").addEventListener("input", function () {
        var i = document.getElementById("CNPJ").value.length;
        var str = document.getElementById("CNPJ").value
        if (isNaN(Number(str.charAt(i - 1)))) {
            document.getElementById("CNPJ").value = str.substr(0, i - 1);
        } else if (str.charAt(i - 1) ===' ') {
            document.getElementById("CNPJ").value = str.substr(0, i - 1);
        }
    });


    if (i === 18) {
        if (valida_cnpj(document.getElementById("CNPJ").value)) {
            
            document.getElementById("feedback").className = "valid-feedback d-block";
            document.getElementById("feedback").innerText ="Cnpj Válido"
            document.getElementById("btnPesquisar").disabled = false;
            document.getElementById("btnPesquisarList").disabled = false;
            document.getElementById("btnAddList").disabled = false;
        } else {
            document.getElementById("feedback").className = "invalid-feedback d-block";
            document.getElementById("feedback").innerText = "Cnpj inválido"
            document.getElementById("btnPesquisar").disabled = true;
            document.getElementById("btnAddList").disabled = true;
            document.getElementById("btnPesquisarList").disabled = true;
        }
    } else {
        document.getElementById("feedback").className = "invalid-feedback d-block";
        document.getElementById("feedback").innerText = "Cnpj inválido"
        document.getElementById("btnPesquisar").disabled = true;
        document.getElementById("btnAddList").disabled = true;
        document.getElementById("btnPesquisarList").disabled = true;
        
    }

});






 

/*
 calc_digitos_posicoes
 
 Multiplica dígitos vezes posições
 
 @param string digitos Os digitos desejados
 @param string posicoes A posição que vai iniciar a regressão
 @param string soma_digitos A soma das multiplicações entre posições e dígitos
 @return string Os dígitos enviados concatenados com o último dígito
*/
function calc_digitos_posicoes(digitos, posicoes = 10, soma_digitos = 0) {

    // Garante que o valor é uma string
    digitos = digitos.toString();

    // Faz a soma dos dígitos com a posição
    // Ex. para 10 posições:
    //   0    2    5    4    6    2    8    8   4
    // x10   x9   x8   x7   x6   x5   x4   x3  x2
    //   0 + 18 + 40 + 28 + 36 + 10 + 32 + 24 + 8 = 196
    for (var i = 0; i < digitos.length; i++) {
        // Preenche a soma com o dígito vezes a posição
        soma_digitos = soma_digitos + (digitos[i] * posicoes);

        // Subtrai 1 da posição
        posicoes--;

        // Parte específica para CNPJ
        // Ex.: 5-4-3-2-9-8-7-6-5-4-3-2
        if (posicoes < 2) {
            // Retorno a posição para 9
            posicoes = 9;
        }
    }

    // Captura o resto da divisão entre soma_digitos dividido por 11
    // Ex.: 196 % 11 = 9
    soma_digitos = soma_digitos % 11;

    // Verifica se soma_digitos é menor que 2
    if (soma_digitos < 2) {
        // soma_digitos agora será zero
        soma_digitos = 0;
    } else {
        // Se for maior que 2, o resultado é 11 menos soma_digitos
        // Ex.: 11 - 9 = 2
        // Nosso dígito procurado é 2
        soma_digitos = 11 - soma_digitos;
    }

    // Concatena mais um dígito aos primeiro nove dígitos
    // Ex.: 025462884 + 2 = 0254628842
    var cpf = digitos + soma_digitos;

    // Retorna
    return cpf;

} // calc_digitos_posicoes
 
/*
 valida_cnpj
 
 Valida se for um CNPJ
 
 @param string cnpj
 @return bool true para CNPJ correto
*/
function valida_cnpj(valor) {

    // Garante que o valor é uma string
    valor = valor.toString();

    // Remove caracteres inválidos do valor
    valor = valor.replace(/[^0-9]/g, '');


    // O valor original
    var cnpj_original = valor;

    // Captura os primeiros 12 números do CNPJ
    var primeiros_numeros_cnpj = valor.substr(0, 12);

    // Faz o primeiro cálculo
    var primeiro_calculo = calc_digitos_posicoes(primeiros_numeros_cnpj, 5);

    // O segundo cálculo é a mesma coisa do primeiro, porém, começa na posição 6
    var segundo_calculo = calc_digitos_posicoes(primeiro_calculo, 6);

    // Concatena o segundo dígito ao CNPJ
    var cnpj = segundo_calculo;

    // Verifica se o CNPJ gerado é idêntico ao enviado
    if (cnpj === cnpj_original) {
        return true;
    }

    // Retorna falso por padrão
    return false;

} // valida_cnpj
 