$("#content").delegate(".cookShopItem", "click", function (e) { //при щелчке на название столовой на карточке
    getCategoryList(e.target.id);                                //получаем список категорий, блюда столовой и статус открыто/закрыто
    getDishByCookShopCards(e.target.id);
    getCookShopInfo(e.target.id);
});

$("#cslist").delegate(".ctgListItem", "click", function (e) { //тоже самое, но при выборе столовой из выпадающего меню
    getCategoryList(e.target.id);
    getDishByCookShopCards(e.target.id);
    getCookShopInfo(e.target.id);
});

$('#ctgList').delegate(".ctgListItem", "click", function (e) { //получаем и отображаем блюда при выборе категории 
    getDishByCategoryCards(e.target.id);
});

$("#content").delegate('.tabs input', "click", function(e){//при щелчке по размеру порцаа
    $(`#t_${$(e.target).attr('class')} h2 a span`).attr("id", e.target.id); //ставим ИД у кнопки купить как у данной порции
    $(`#t_${$(e.target).attr('class')} h2 a span`).attr("data-price", $(e.target).attr('data-price'));// тоже самое с ценой
    $(`#t_${$(e.target).attr('class')} h2 a span`).attr("data-size", $(e.target).attr('data-size'));//размером
    $(`#price_${$(e.target).attr('class')}`).text($(e.target).attr('data-price') + ' руб');//показываем цену порции
});

$("#content").delegate('.ti-shopping-cart', "click", function(e){//при щелчке по кнопке купить
    if(e.target.id !="none"){
        addToCart(e); //если порция выбрана, то добавляем в корзину
    }
    
    let that = $(e.target).closest('.product').find('img'); //.find('data-img');
    console.log(that);
    that.clone()
        .css({
            'position': 'absolute',
            'z-index': '11100',
            top: $(this).offset().top - 250,
            left: $(this).offset().left - 75,
            'width': 100,
            'height': 50,
        })
        .appendTo("body")
        .animate({
            opacity: 0.05,
            left: $(".ti-shopping-cart").offset()['left'] + 75,
            top: $(".ti-shopping-cart").offset()['top'],
            width: 20
        }, 1500, function () {
            $(this).remove();
        });   
});

$('#show_cart').click(function(){ //при щелчке по значку корзины справа сверху, показываем актуальное содержимое корзины
    getCart();
});

$('#clean_card').click(function(){ //при щелчке на очистить в корзине, очищаем корзину
    cleanCart();
});

$('.shopping__cart').delegate(".close","click", function (e){ //при нажатие крестика на элементе корзины, удаляем этот элемент из
    removeItem($(e.target).attr('data-id'));                  //корзины
});
