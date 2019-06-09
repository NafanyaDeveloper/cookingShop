function getDishByCookShopCards(id) { //получаем все блюда столовой
    $('#content').html(getLoader());
    $.ajax(
        {
            type: 'GET',
            url: `/dish/DishesByCookShopList/${id}`,
            dataType: "html",
            success: function (data, textStatus) {
                $('#content').html(data);
            }
        });
};

function getDishByCategoryCards(id) { //получаем блюда, в зависимости от категории
    $('#content').html(getLoader());
    $.ajax(
        {
            type: 'GET',
            url: `/dish/DishesByCategoryList/${id}`,
            dataType: "html",
            success: function (data, textStatus) {
                $('#content').html(data);
            }
        });
};