window.addEventListener('load', function() {
    var xhr1 = new XMLHttpRequest();
    xhr1.open('GET', '/checkloginstatus', true);

    xhr1.onreadystatechange = function () {
        if (xhr1.readyState === 4 && xhr1.status === 200) {
            var response = JSON.parse(xhr1.responseText);
            console.log(response);
            if (response.username) {
                // Если пользователь аутентифицирован, заменит ссылки на "Кабинет"
                var wellomemsg = document.getElementById('UserName');
                wellomemsg.innerHTML = response.username;
            }
        }
    };

    xhr1.send();
    
    var xhr2 = new XMLHttpRequest();
    xhr2.open('GET', '/getusercourses', true);

    console.log("freferfgefr");

    xhr2.onreadystatechange = function () {
        if (xhr2.readyState === 4) {
            if (xhr2.status === 200) {
                var response = JSON.parse(xhr2.responseText);
                console.log(response);

                var courseContainer = document.getElementsByClassName('centered-container')[0];

                courseContainer.innerHTML = '';

                response.forEach(function (course) {
                    course = JSON.parse(course);
                    console.log(course);
                    courseContainer.innerHTML += `
                        <a href="/course?id=${course.id}&page=0" class="course-link">
                            <div class="course">
                                <p>курс</p>
                                <p>•</p>
                                <p>куплен</p>
                                <h2 class="course-name">${course.name}</h2>
                                <p class="course-description">${course.description}</p>
                                <br>
                                <p class="course-duration">${course.duration} серий</p>
                            </div>
                        </a>
                    `;
                });
            }
            else {
                console.error('Ошибка при запросе /getallcourses');
            }
        }
    };

    xhr2.send();

});
