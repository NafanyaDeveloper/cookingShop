// здесь логика для корзины. для хранения данных используется сессия браузера

function getCartData() {
  return JSON.parse(sessionStorage.getItem('cart')); //достаем данные из сессии
}

function cleanCart(){ //очищаем корзину
    sessionStorage.clear();//очистка корзины
    getCart(); //обновление информации о корзине
}

function setCartData(i){ //тобавление товара в корзину
  sessionStorage.setItem('cart', JSON.stringify(i)); //добавление в сессию 
  return false;
}

function addToCart(e) //получить и отобразить корзину
{
    let cartData = getCartData() || {}; //получить данные из сессии либо же пустой список, если данных нет
    let img = $(e.target).attr('data-img'); //получаем изображение 
    let clr = $(e.target).attr('data-clr'); //калории
    let pr = $(e.target).attr('data-pr');  //белки
    let crb = $(e.target).attr('data-crb'); //углеволы
    let fat = $(e.target).attr('data-fat'); //жиры
    let title = $(e.target).attr('data-title');//название
    let price = $(e.target).attr('data-price');//цену
    let size = $(e.target).attr('data-size');//размер
    let id = e.target.id; //ид
    if(cartData.hasOwnProperty(id)) //если товар с таким ИД есть, то мы добавляем к количеству +1
        cartData[id][0] += 1;
    else
        cartData[id] = [1, title, img, price, size, clr, pr, crb, fat]; //иначе добавляем новый
    setCartData(cartData); //сохраняем в сессию
}

function getCart(){
    let cart = "";
    let price = 0; //сюда будем прибавлять цену каждой единицы, чтобы отобразить в итоговой цене
    let clr = 0; //аналогично для каллорий 
    let cartData = getCartData() || {};
    for(var items in cartData)
    {
        cart += getCartString(cartData[items], items); //передаем данные о товаре для макета верстки 
        price += Number(cartData[items][0]) * Number(cartData[items][3]); //прибавляем цену
        clr += Number(cartData[items][5] * cartData[items][4]/100 * cartData[items][0]); //калории
    }
    $('#cart_content').html(cart); //обновляем корзину на странице
    $('#total__price').html(price + ' руб'); //отображаем стоимость
    $('#shp__calories').html(clr + ' ккал'); // и калориии
}

function removeItem(id){ //удалить товар из корзины 
    let cartData = getCartData() || {}; //получаем содержимое 
    delete cartData[id]; //удаляем нужный элемент
    setCartData(cartData); //сохраняем в сессию то, что получилось
    getCart();  //отображаем в корзине
}