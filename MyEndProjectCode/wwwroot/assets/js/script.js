
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



    $(document).on("click", ".allprod", function (e) {
        e.preventDefault();



        let parent = $(".product-grid-view")

        
        $.ajax({

            url: "shop/GetAllProduct",
            type: "Get",

            success: function (res) {

                $(parent).html(res)
            }
        })

    })



    $(document).on("click", ".brandcl", function (e) {
        e.preventDefault();

        let brandId = $(this).attr("data-id");

        let product = $(".product-grid-view")


        let paginate = $(".pagination-area")


        


        $.ajax({

            url: `shop/GetProductsByBrandName?id=${brandId}`,
            type: "Get",

            success: function (res) {

                $(product).html(res)

                $(paginate).addClass("d-none")
               
            }
        })

    })



    $(document).on("click", ".tagn", function (e) {
        e.preventDefault();

        let tagId = $(this).attr("data-id");

        let product = $(".product-grid-view")


        let paginate = $(".pagination-area")





        $.ajax({

            url: `shop/GetProductsTagName?id=${tagId}`,
            type: "Get",

            success: function (res) {

                $(product).html(res)

                $(paginate).addClass("d-none")

            }
        })

    })




})
