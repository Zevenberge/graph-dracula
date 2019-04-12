let details = document.getElementById('details');

class FilmCreateData {
    canSubmit() {
        return this.name && this.releaseYear && this.country;
    }
}

class FilmChangeSet {
    constructor(id) {
        this.id = id;
    }

    canSubmit() {
        return true;
    }
}

async function createFilm() {
    let result = await fetch(`/film/new`);
    let page = await result.text();
    details.innerHTML = page;
    details.changeSet = new FilmCreateData();
}

async function filmSelected(id) {
    console.log(`Selected ${id}`);
    let result = await fetch(`/film/${id}`);
    let page = await result.text();
    details.innerHTML = page;
    details.changeSet = new FilmChangeSet(id);
}

function inputChanged(name, newValue) {
    console.log(name + " changed: " + newValue);
    details.changeSet[name] = newValue;
    let button = document.getElementById('submit');
    button.disabled = !details.changeSet.canSubmit();
}

async function submit(button) {
    button.disabled = true;
    let response = await postAsync('/api/film/edit');
    if(!response.ok) {
        button.disabled = false;
        throw "It's dead Jim";
    }
}

async function create(button) {
    button.disabled = true;
    let response = await postAsync('/api/film/create');
    if(!response.ok) {
        button.disabled = false;
        throw "It's dead Jim";
    } else {
        let content = await response.json();
        await filmSelected(content);
    }
}

function postAsync(url) {
    return fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({dto: details.changeSet})
    });
}