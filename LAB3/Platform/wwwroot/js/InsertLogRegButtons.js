window.addEventListener('load', function() {
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/checkloginstatus', true);

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            var response = JSON.parse(xhr.responseText);
            console.log(response);
            if (response.loginstatus) {
                // Если пользователь аутентифицирован, заменит ссылки на "Кабинет"
                var authLinks = document.getElementById('authLinks');
                authLinks.innerHTML = '<li><a href="/cabinet">Кабинет</a></li>';
            }
            else {
                var authLinks = document.getElementById('authLinks');
                authLinks.innerHTML = '<li><a href="/login">Вход</a></li><li><a href="/register">Регистрация</a></li>';
            }
        }
    };

    xhr.send();
});