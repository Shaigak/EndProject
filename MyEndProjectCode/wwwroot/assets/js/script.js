
$(function () {

    $(document).on("click", ".cate", function (e) {
        e.preventDefault();

        let categoryId = $(this).attr("data-id");

        let product = $(".product-grid-view")

        let paginate = $(".pagination-area")

        $.ajax({

            url: `shop/GetProductsByCategory?id=${categoryId}`,
            type: "Get",

            success: function (res) {

                $(product).html(res)

                $(paginate).addClass("d-none")
               /* console.log(res)*/
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
               

               /* debugger*/
                swal.fire({
                    icon: 'success',
                    title: 'product added',
                    showconfirmbutton: false,
                    timer: 1500,
                })
            

                $(".shaiq").text(res)

              /*  console.log($("#basketCount").html(100))*/

               
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


                /*debugger*/
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




    //$(document).on("click", ".topla", function () {
    //    //let id = $(this).attr('data-id');
    //    //e.preventDefault();

    //    let id = $(this).parent().parent().parent().attr('id')

    //    let count = $(this).prev().val();


    //    $.ajax({
    //        method: "POST",
    //        url: "/basket/IncrementProductCount",
    //        data: {
    //            id: id
    //        },
    //        content: "application/x-www-from-urlencoded",
    //        success: function (res) {

                 
    //                 window.location.reload();
             
              

    //        }
    //    });
    //});

    //$(document).on("click", ".cix", function () {
   

    //    let id = $(this).parent().parent().parent().attr('id')

    //    let count = $(this).next().val();

    //    debugger


    //    $.ajax({
    //        method: "POST",
    //        url: "/basket/DecrementProductCount",
    //        data: {
    //            id: id
    //        },
    //        content: "application/x-www-from-urlencoded",
    //        success: function (res) {

    //            //if (count != 1) {
    //            //    count--;
    //            //    $(".cix").next().val(count);
    //            //}

    //            if (count != 1) {
    //                window.location.reload();
    //            }
               

    //        }
    //    });
    //});


    $(document).on("click", ".cix", function () {
   
        let id = $(this).parent().parent().parent().attr("id");
        debugger
        let inputValue = $(this).next().val();
       

        let input = $(this).next();
        if (inputValue != 1) {
            inputValue--;
            $(input).val(inputValue);
        }

        console.log(input)
        console.log(inputValue)
        $.ajax({
            method: "POST",
            url: "/basket/DecrementProductCount",
            data: {
                id: id
            },
            content: "application/x-www-from-urlencoded",
            success: function (res) {
                subTotal();
            }
        });
    });


    $(document).on("click", ".topla", function () {

        let id = $(this).parent().parent().parent().attr("id");
        debugger
        let inputValue = $(this).prev().val();
        let nativePrice = $(this).parent().parent().next().text();
        let subtotalPrice = $(this).parent().parent().next().next().text();
        let count = $(this).prev().val();

        let input = $(this).prev();
        
            inputValue++;
            $(input).val(inputValue);

        $.ajax({
            method: "POST",
            url: "/basket/IncrementProductCount",
            data: {
                id: id
            },
            content: "application/x-www-from-urlencoded",
            success: function (res) {
                subTotal(res, nativePrice, subtotalPrice, count);
                grandTotal();
            }
        });
    });



    //function subTotal() {
    //    let tbody = $(".tbody").children()

    //    let head=0

    //    for (var prod of tbody) {
    //        let price = parseFloat($(prod).children().eq(4).text())

    //        let count = parseFloat($(prod).children().eq(3).childiren().eq(1).val());

    //       head= price * total
    //    }

    //    $(".grand-total").text(head);
    //}


    

    function subTotal(res, nativePrice, subtotalPrice, count) {
        $(count).val(res);
        let subtotal = parseFloat(nativePrice * $(count).val());
        $(total).text(subtotal);
    }

    function grandTotal() {
        let tbody = $(".tbody").children()

        let sum = 0;
        for (var prod of tbody) {
            let price = parseFloat($(prod).children().eq(5).text())
            sum += price
        }
        $(".grand-total").text(sum + ".00");
    }




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



    $(document).on("click", ".delete-wishlist", function () {
        //var basketCount = $('.shaiq')
        let id = $(this).parent().parent().attr("data-id");

      /*  console.log(id)*/

        let tr = $(this).parent().parent();
     /*   console.log(tr)*/
        let data = { id: id };
        var basketCurrentCount = $('.basketCount').html()
       /* var id = $(this).attr('data-id');*/
        var quantity = $(this).attr('data-quantity')
        var sum = basketCurrentCount - quantity

        let tbody = $(".tbody").children();

        let tablebasket = $(".tablebasket")



        $.ajax({
            method: 'POST',
            url: "/wish/delete",
            data: data,
            success: function (res) {
                $(tr).remove()
                Swal.fire({
                    icon: 'success',
                    title: 'Product deleted',
                    showConfirmButton: false,
                    timer: 1500
                })

                if ($(tbody).length == 1) {
                    $(tablebasket).addClass("d-none");
                    $(".seeclass").removeClass("d-none")

                }



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

            //console.log(price)

             
        }
        $(".grand-total").text(sum);

     
       
    }


    //function subTotal() {
    //    let tbody = $(".tbody").children()

    //    let head=0

    //    for (var prod of tbody) {
    //        let price = parseFloat($(prod).children().eq(4).text())

    //        let total = parseFloat($(prod).children().eq(5).text())

    //       head= price * total

      

    //    }

    //    $(".grand-total").text(head);
    //}



    //$(document).on("click", ".incredit", function (e) {
    //    e.preventDefault();


    //    console.log("S")


    //  /*  let parent = $(".product-grid-view")*/


    //    //$.ajax({

    //    //    url: "shop/GetAllProduct",
    //    //    type: "Get",

    //    //    success: function (res) {

    //    //        $(parent).html(res)
    //    //    }
    //    //})

    //}) 

   




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
            //console.log(pricec)
          
           
            
           
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



    //$(document).on("click", ".sendmes", function (e) {
   


    //    $.ajax({

    //        url: `shop/GetProductsTagName?id=${tagId}`,
    //        type: "Get",

    //        success: function (res) {


    //        }
    //    })

    //})



    $(document).on("submit", "#filterForm", function (e) {
        e.preventDefault();
        let value1 = $(".min-price").val()
        let value2 = $(".max-price").val()

        let paginate = $(".pagination-area")

      
        //console.log(value1)
        //console.log(value2)

        let data = { value1: value1, value2: value2 }
        let parent = $(".product-grid-view")
        $.ajax({
            url: "/Shop/GetRangeProducts",
            type: "Get",
            data: data,
            success: function (res) {
                $(parent).html(res);
                if (value1 == "10" && value2 == "500") {
                     $(paginate).addClass("d-none")


                }

            }

        })
    })


    let minValue = document.getElementById("min-value");
    // console.log(minValue);
    let maxValue = document.getElementById("max-value");

    function validateRange(minPrice, maxPrice) {
        if (minPrice > maxPrice) {
            // Swap to Values
            let tempValue = maxPrice;
            maxPrice = minPrice;
            minPrice = tempValue;
        }

        minValue.innerHTML = "$" + minPrice;
        maxValue.innerHTML = "$" + maxPrice;
    }

    const inputElements = document.querySelectorAll(".range-slider input");
    inputElements.forEach((element) => {
        element.addEventListener("change", (e) => {
            let minPrice = parseInt(inputElements[0].value);
            let maxPrice = parseInt(inputElements[1].value);

            validateRange(minPrice, maxPrice);
        });
    });

    validateRange(inputElements[0].value, inputElements[1].value);







































































































































































})
