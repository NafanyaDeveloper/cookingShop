function getCartString(elem, id){ //макет html для корзины 
    return `<div class="shp__single__product">
                <div class="shp__pro__thumb">
                    <a href="#">
                        <img src="${elem[2]}" alt="product images">
                    </a>
                </div>
                <div class="shp__pro__details">
                    <h2><a href="product-details.html">${elem[1]}</a></h2>
                    <span class="quantity">Количество<div class="quantity_food">${elem[0]}</div></span>
                    <span class="shp__calories">Калории<div class="quantity_calories">${elem[5] * elem[4]/100} x ${elem[0]}</div></span>
                    <span class="shp__calories">Белки<div class="quantity_calories">${elem[6]} %</div></span>
                    <span class="shp__calories">Жиры<div class="quantity_calories">${elem[8]} %</div></span>
                    <span class="shp__calories">Углеводы<div class="quantity_calories">${elem[7]} %</div></span>

                    <span class="price">Цена<div class="price_food">${elem[3]} x ${elem[0]} = ${elem[3] * elem[0]} руб</div></span>
                </div>
                <div class="remove__btn">
                    <a href="#" title="Remove this item"><button type="button" class="close" aria-label="Close"><span aria-hidden="true"  data-id="${id}">×</span></button></a>
                </div>
            </div>`;
};

function getLoader() { // макет лоадера
    return `<div class="col-lg-9 col-md-9 col-sm-12 col-xs-12 loader"></div>`;
}

//cartData[id] = [1, title, img, price, size, clr, pr, crb, fat];
//                0    1     2     3      4    5   6    7    8е