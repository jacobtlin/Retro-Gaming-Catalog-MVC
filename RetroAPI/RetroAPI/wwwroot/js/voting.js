var voting = function (productName) {

    //
    // Get the additional Information that we are storing
    // on the client side.
    // 
    var extraProductInfo = [];
    $.getJSON("assets/extraProductInfo.json", function (data) {
        //return data;
        $.each(data, function (key, val) {
            extraProductInfo.push(val);
        });
    });


    //
    // Get all the Products Information via the api
    //
    let productInfoesAPI = 'api/TblProductInfoes';
    let request = new XMLHttpRequest();
    request.open('GET', productInfoesAPI);
    request.send();
    request.addEventListener('load', function () {
        let productArray = JSON.parse(request.responseText);
        console.log(productArray);

        //
        // Get all of the ranking information via api
        //
        let productRankingsAPI = 'api/TBLProductRankings';
        let requestR = new XMLHttpRequest();
        requestR.open('GET', productRankingsAPI);
        requestR.send();
        requestR.addEventListener('load', function () {
            let rankingsArray = JSON.parse(requestR.responseText);

            var foundProduct = $.grep(extraProductInfo, function (n, i) {
                return n.productName == "Pitfall";
            });

            foundProduct[0].productVotes += 1;

            extraProductInfo.sort(function (a, b) { return b.productVotes - a.productVotes });

            console.log(extraProductInfo);

            /*console.log(rankingsArray);

            rankingsArray.sort(function (a, b) { return a.productRanking - b.productRanking });

            console.log(rankingsArray);*/


            //
            // For each extraProductInfo file
            //
            $.each(extraProductInfo, function (i, product) {
                //
                // Find the ranking for this extra product info
                //
                var foundRanking = $.grep(rankingsArray, function (n, i) {
                    return n.productName == product.productName;
                });
                foundRanking[0].productRanking = i+1;
            });

            //
            // update the new rankings via API
            //
            $.each(rankingsArray, function (i, ranking) {
                //
                // turn the array item into a single Json string
                //
                var json = JSON.stringify(ranking);

                let putAPI = productRankingsAPI + '/' + ranking.productId.toString();
                requestR.open('PUT', putAPI, true);
                requestR.setRequestHeader('Content-type', 'application/json; charset=utf-8');
                requestR.onload = function () {
                    var users = JSON.parse(requestR.responseText);
                    if (requestR.readyState == 4 && requestR.status == "200") {
                        console.log("state 4 status 200");
                        console.log(users);
                    } else {
                        console.log("Draw Bad Card!!");
                        console.log(users);
                    }

                }
                requestR.send(json);

            });

        });

    });

 
};