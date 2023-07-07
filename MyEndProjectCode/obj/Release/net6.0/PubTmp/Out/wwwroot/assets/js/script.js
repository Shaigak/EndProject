
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



    $(document).on("click", ".add-baskets", function () {
        let id = $(this).attr('data-id');



        $.ajax({
            method: "POST",
            url: "/wish/addbasket",
            data: {
                id: id
            },
            content: "application/x-www-from-urlencoded",
            success: function (res) {


                debugger
                swal.fire({
                    icon: 'success',
                    title: 'Product added to your wishlist ',
                    showconfirmbutton: false,
                    timer: 1500,
                })


                //$(".shaiq").text(res)

                //console.log($("#basketCount").html(100))


            }
        });
    });

    $(document).on('click', '#deleteBtn', function () {
        var id = $(this).attr('data-id')
        var basketCount = $('.shaiq')

        var basketCountdelete = $('.shaiq').html();

        
        var basketCurrentCount = $('.basketCount').html()
        var id = $(this).attr('data-id');
        var quantity = $(this).attr('data-quantity')
        var sum = basketCurrentCount - quantity

        let tbody = $(".tbody").children();

       



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

                grandTotal();

                if ($(tbody).length == 1) {
                    $(".tablebasket").addClass("d-none");
                       $(".seeclass").removeClass("d-none")

                }

                $(`.basket-product[id=${id}]`).remove();
                $('.shaiq').html("")
                $('.shaiq').html(res)
            
                grandTotal();

                //$('.basketCountdelete') == 0 ? $('tablebasket').addClass("d-none")

              
                
            }
        })

    })



    $(document).on('click', '#deleteBtns', function () {
        var id = $(this).attr('data-id')
        var basketCount = $('.shaiq')

        var basketCountdelete = $('.shaiq').html();


        var basketCurrentCount = $('.basketCount').html()
        var id = $(this).attr('data-id');
        var quantity = $(this).attr('data-quantity')
        var sum = basketCurrentCount - quantity

        let tbody = $(".tbody").children();





        $.ajax({
            method: 'POST',
            url: "/wish/delete",
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

                if ($(tbody).length == 1) {
                    $(".tablebasket").addClass("d-none");
                    $(".seeclass").removeClass("d-none")

                }

                //$(`.basket-product[id=${id}]`).remove();
                //$('.shaiq').html("")
                //$('.shaiq').html(res)

                /*grandTotal();*/

                //$('.basketCountdelete') == 0 ? $('tablebasket').addClass("d-none")



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

    $(document).on("submit", ".hm-searchbox", function (e) {
        e.preventDefault();
        let value = $(".input-search").val();
        let url = `/shop/MainSearch?searchText=${value}`;

        window.location.assign(url);

    })


    function grandTotal() {
        let tbody = $(".tbody").children()
        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(5).text())
            sum += price

            console.log(price)

             
        }
        $(".grand-total").text(sum);

     
       
    }




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


    $(document).ready(function () {


        var min= $(".minus")
        $('.minus').click(function () {
            var $input = $(this).parent().find('input');
            var count = parseInt($input.val()) - 1;
            count = count < 1 ? 1 : count;
           
            let tbody = $(".tbody").children()
            var counta = $(min).next().val()

            let pricec = parseFloat($(tbody).children().eq(4).text())
            console.log(pricec)
          
           
            
           
            $input.val(count);
            $input.change();
            return false;
        });
        $('.plus').click(function () {
            var $input = $(this).parent().find('input');
            $input.val(parseInt($input.val()) + 1);
            $input.change();
            return false;
        });
    });



    $(document).on("click", ".minus", function () {
        let id = $(this).parent().parent().parent().attr("data-id");
        let nativePrice = parseFloat($(this).parent().parent().prev().children().eq(1).text());
        let total = $(this).parent().parent().next().children().eq(1);
        let count = $(this).prev().prev();

        $.ajax({
            type: "Post",
            url: `Cart/IncrementProductCount?id=${id}`,
            success: function (res) {
                res++;
                subTotal(res, nativePrice, total, count)
                grandTotal();
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
