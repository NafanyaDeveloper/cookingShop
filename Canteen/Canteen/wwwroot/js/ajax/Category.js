function getCategoryList(id) { //�������� ������ ��������� ��� ���� � ������� AJAX
    $.ajax(
        {
            type: 'GET',
            url: `/category/CategoriesList/${id}`,
            dataType: "html",
            success: function (data, textStatus) {
                $('#ctgList').html(data); //���������� � ����
            }
        });
};