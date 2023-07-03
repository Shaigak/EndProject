
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

    });

    $(document).on("click", ".add-basket", function () {
        let id = $(this).attr('data-id');
       
      
        $.ajax({
            method: "POST",
            url: "/basket/addbasket",
            data: {
                id: id
            },
            content: "application/x-www-from-urlencoded",
            success: function (res) {
               

                debugger
                swal.fire({
                    icon: 'success',
                    title: 'product added',
                    showconfirmbutton: false,
                    timer: 1500,
                })
            

                $(".shaiq").text(res)

                console.log($("#basketCount").html(100))

               
            }
        });
    });

    $(document).on('click', '#deleteBtn', function () {
        var id = $(this).attr('data-id')
        var basketCount = $('.shaiq')
        var basketCurrentCount = $('.basketCount').html()
        var id = $(this).attr('data-id');
        var quantity = $(this).attr('data-quantity')
        var sum = basketCurrentCount - quantity


        $.ajax({
            method: 'POST',
            url: "/basket/delete",
            data: {
                id: id
            },
            success: function (res) {

                Swal.fire({
                    icon: 'success',
                    title: 'Product deleted',
                    showConfirmButton: false,
                    timer: 1500
                })

                $(`.basket-product[id=${id}]`).remove();
                $('.shaiq').html("")
                $('.shaiq').html(res)

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
