function getCategoryList(id) { //получаем список категорий для меня с помощью AJAX
    $.ajax(
        {
            type: 'GET',
            url: `/category/CategoriesList/${id}`,
            dataType: "html",
            success: function (data, textStatus) {
                $('#ctgList').html(data); //отображаем в меню
            }
        });
};