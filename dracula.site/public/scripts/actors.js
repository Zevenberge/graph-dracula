let details = document.getElementById('details');

class ActorCreateData {
    canSubmit() {
        return this.name && this.dateOfBirth && this.nationality;
    }
}

class ActorChangeSet {
    constructor(id) {
        this.id = id;
    }

    canSubmit() {
        return true;
    }
}

class ContributionChangeSet {
    constructor(id) {
        this.actor = id;
    }

    canSubmit() {
        return this.film && this.role;
    }
}

async function createActor() {
    let result = await fetch(`/actor/new`);
    let page = await result.text();
    details.innerHTML = page;
    details.changeSet = new ActorCreateData();
}

async function actorSelected(id) {
    console.log(`Selected ${id}`);
    let result = await fetch(`/actor/${id}`);
    let page = await result.text();
    details.innerHTML = page;
    details.changeSet = new ActorChangeSet(id);
}

function inputChanged(name, newValue) {
    console.log(name + " changed: " + newValue);
    details.changeSet[name] = newValue;
    let button = document.getElementById('submit');
    button.disabled = !details.changeSet.canSubmit();
}

async function submit(button) {
    button.disabled = true;
    let response = await postAsync('/api/actor/edit', details.changeSet);
    if(!response.ok) {
        button.disabled = false;
        throw "It's dead Jim";
    }
}

async function create(button) {
    button.disabled = true;
    let response = await postAsync('/api/actor/create', details.changeSet);
    if(!response.ok) {
        button.disabled = false;
        throw "It's dead Jim";
    } else {
        let content = await response.json();
        await actorSelected(content);
    }
}

async function addFilm(button) {
    let response = await fetch(`/actor/add`);
    if(response.ok) {
        let content = await response.text();
        let element = document.getElementById('newFilm');
        element.innerHTML = content;
        details.contribution = new ContributionChangeSet(details.changeSet.id);
    }
}

function inputContributionChanged(name, newValue) {
    details.contribution[name] = newValue;
    let button = document.getElementById('save');
    button.disabled = !details.contribution.canSubmit();
}

async function saveContribution(button) {
    button.disabled = true;
    let response = await postAsync('/api/actor/contribution', details.contribution);
    if(!response.ok) {
        button.disabled = false;
        throw "Not now son";
    } else {
        let content = await response.json();
        await actorSelected(content);
    }
}

function cancelContribution() {
    details.contribution = undefined;
    let element = document.getElementById('newFilm');
    element.innerHTML = "";
}

function postAsync(url, data) {
    return fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({dto: data})
    });
}