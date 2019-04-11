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
    let response = await postAsync('/api/actor/edit');
    if(!response.ok) {
        button.disabled = false;
        throw "It's dead Jim";
    }
}

async function create(button) {
    button.disabled = true;
    let response = await postAsync('/api/actor/create');
    if(!response.ok) {
        button.disabled = false;
        throw "It's dead Jim";
    } else {
        let content = await response.json();
        await actorSelected(content);
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