function getCookShopList()
{
    $.ajax(
        {
            type: 'GET',
            url: '/cookshop/CookShopList',
            dataType: "html",
            success: function (data, textStatus) {
                $('#cslist').html(data);
            }
        });
};

function getCookShopCards() { //получаем и отображаем карточки столовых с помощью AJAX
    $('#content').html(getLoader());//запускаем лоадер, пока мы получаем данные
    $.ajax(
        {
            type: 'GET',
            url: '/cookshop/CookShopCards',
            dataType: "html",
            success: function (data, textStatus) {
                $('#content').html(data); //отображаем данные на странице
            }
        });
};

function getCookShopInfo(id){ //получаем название, а так же часы работы и состояние открыто/закрыто
    $.ajax(
        {
            type: 'GET',
            url: `/cookshop/CookShopInfo/${id}`,
            dataType: "json",
            success: function (data, textStatus) {
                $('#cs_logo').html(`<h3 class="logo">${data.title}</h3>`); //отображаем лого
                if(data.isOpen) // если тру, то открыто, иначе закрыто
                    $('#cs_time').html(`<h2 style="color:green">Открыто</h2>`);
                else
                     $('#cs_time').html(`<h2 style="color:red">Закрыто</h2>`);
            }
        });
}