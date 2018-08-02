// On load
window.onload = function () {
    getHeroes();
};

// Populate using AJAZ
function getHeroes() {
    var requestURL = 'https://mdn.github.io/learning-area/javascript/oojs/json/superheroes.json';
    var request = new XMLHttpRequest();
    request.open('GET', requestURL);
    request.responseType = 'json';
    request.send();
    request.onload = function () {
        let data = request.response;
        populateHeader(data);
        showHeroes(data);
    }
}

function populateHeader(data) {
    let header = document.querySelector("header");

    let h3 = document.createElement('h3');
    h3.textContent = "Super Hero Squad";

    let teamInfo = document.createElement('p');
    teamInfo.textContent = "Hometown: " + data.homeTown + " // Formed: " + data.formed;

    header.appendChild(h3);
    header.appendChild(teamInfo);
}

function showHeroes(data) {
    var section = document.querySelector("section");

    for (hero in data.members) {
        var article = document.createElement('article');

        var name = document.createElement('h2');
        var identity = document.createElement('p');
        var age = document.createElement('p');
        var powers = document.createElement('p');
        var powerList = document.createElement('ul');

        name.textContent = data.members[hero].name;
        identity.textContent = 'Identity: ' + data.members[hero].secretIdentity;
        age.textContent = 'Age: ' + data.members[hero].age;
        powers.textContent = 'Powers: ';

        for (power in data.members[hero].powers) {
            let item = document.createElement('li');
            item.textContent = data.members[hero].powers[power];
            powerList.appendChild(item);
        }

        article.appendChild(name);
        article.appendChild(identity);
        article.appendChild(age);
        article.appendChild(powers);
        article.appendChild(powerList);

        section.appendChild(article);
    }
}
