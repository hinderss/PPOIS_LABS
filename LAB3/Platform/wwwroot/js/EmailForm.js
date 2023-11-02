document.getElementById("submit").addEventListener("click", function() {
    const email = document.getElementById("email").value;
    const formData = new FormData();
    formData.append("email", email);

    fetch("/subscribe", {
        method: "POST",
        body: formData,
    })
    .then(response => response.json())
    .then(data => {
        alert(data.message);
    })
    .catch(error => {
        // Handle errors
        console.error(error);
    });
});