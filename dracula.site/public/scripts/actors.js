let details = document.getElementById('details');

class ActorChangeSet {
    constructor(id) {
        this.id = id;
    }
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
    button.disabled = false;
}

async function submit(button) {
    button.disabled = true;
    var response = await fetch('/api/actor/edit', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({dto: details.changeSet})
    });
    if(!response.ok) {
        button.disabled = false;
        throw "It's dead Jim";
    }
}