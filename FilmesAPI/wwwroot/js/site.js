// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $("#searchLink").click(function (e) {
        e.preventDefault();

        // Toggle classes para exibir/ocultar o ícone e a barra de pesquisa
        $("#searchIcon").toggleClass("d-none");
        $("#searchForm").toggleClass("d-none");
    });

    $("#searchForm").click(function (e) {
        // Impede que o clique na barra de pesquisa propague para o documento
        e.stopPropagation();
    });

    // Adicione esta parte do script para fechar o formulário se clicar fora dele
    $(document).mouseup(function (e) {
        var container = $("#searchForm");

        // Se o alvo do clique não for o container nem um filho do container
        if (!container.is(e.target) && container.has(e.target).length === 0) {
            // Oculte a barra de pesquisa e mostre o ícone
            $("#searchIcon").removeClass("d-none");
            container.addClass("d-none");
        }
    });
});