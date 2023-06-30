
$(function () {
    $(document).on("click", ".cate", function (e) {
        e.preventDefault();

        let categoryId = $(this).attr("data-id");

        let product = $(".product-grid-view")

        $.ajax({

            url: `shop/GetProductsByCategory?id=${categoryId}`,
            type: "Get",

            success: function (res) {
                    
                $(product).html(res)
                console.log(res)
            }
        })

    })

})
