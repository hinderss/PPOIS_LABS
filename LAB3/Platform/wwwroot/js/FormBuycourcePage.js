window.addEventListener('load', function() {

    var url = new URL(window.location.href);

    var value = url.searchParams.get("id");

    var xhr1 = new XMLHttpRequest();
    xhr1.open('GET', '/alreadypurchased?id=' + value, true);

    var alreadypurchased = false;

    xhr1.onreadystatechange = function () {
        if (xhr1.readyState === 4 && xhr1.status === 200) {
            console.log(xhr1.responseText);
            var response2 = JSON.parse(xhr1.responseText);
            

            console.log(response2);

            alreadypurchased = response2.alreadypurchased;

            
            var block = document.getElementsByClassName("right-block")[0];
            
            if (block){
                if (alreadypurchased) {
                    block.innerHTML += (`
                    <a id="buy-button" href="/cabinet"">
                        <button class="buy-button">Вы уже купили этот курс</button>
                    </a>
                    `)
                }
                else{
                    block.innerHTML +=(`
                    <a id="buy-button" href="">
                        <button class="buy-button">Купить</button>
                    </a>
                    `)
                    var buybutton = document.getElementById("buy-button")
                    buybutton.href = `/payment?id=${value}`;
                }
            }
            
        }
    };

    xhr1.send();

    var xhr2 = new XMLHttpRequest();
    xhr2.open('GET', '/getcoursetobuy?id=' + value, true);

    xhr2.onreadystatechange = function () {
        if (xhr2.readyState === 4 && xhr2.status === 200) {
            var response2 = JSON.parse(xhr2.responseText);
            
            response2 = JSON.parse(response2)
            console.log(response2);

            var name = document.getElementById("name");
            var description = document.getElementById("description");
            var skills = document.getElementById("skills");
            var price = document.getElementById("price");

            name.innerHTML = response2.name;
            description.innerHTML = response2.description;
            skills.innerHTML = response2.skills;
            price.innerHTML = "$" + response2.price;

            
        }
    };

    xhr2.send();

    console.log(value);
});