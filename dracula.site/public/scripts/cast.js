deletedContributions = [];
modifiedContributions = {};

function addContribution() {
    let container = document.getElementById("new");
    let actor = container.getElementsByClassName("actor")[0];
    let actorClone = actor.cloneNode(true);
    transferSelection(actor, actorClone);
    let role = container.getElementsByClassName("role")[0];
    let roleClone = role.cloneNode(true);
    transferSelection(role, roleClone);
    let listItem = document.createElement("li");
    listItem.innerHTML = '<div class="delete" onclick="removeNewContribution(this)">X</div>';
    listItem.appendChild(actorClone);
    listItem.appendChild(roleClone);
    let list = document.getElementById("list");
    list.appendChild(listItem);
}

function transferSelection(source, target) {
    for(let i = 0; i < source.options.length; ++i) {
        target.options[i].selected = source.options[i].selected;
        source.options[i].selected = false;
    }
}

function contributionChanged(field, id, value) {
    if(modifiedContributions[id]) {
        modifiedContributions[id][field] = value
    } else {
        modifiedContributions[id] = {
            id: id
        }
        modifiedContributions[id][field] = value
    }
}

function deleteContribution(id, element) {
    deletedContributions.push(id);
    let listItem = element.parentNode;
    let list = listItem.parentNode;
    list.removeChild(listItem);
    modifiedContributions[id] = undefined;
}

function removeNewContribution(button) {
    let listItem = button.parentNode;
    let list = document.getElementById("list");
    list.removeChild(listItem);
}

async function submit(id) {
    let list = document.getElementById("list");
    let additions = readAdditions(list, id);
    let modifications = Object.keys(modifiedContributions)
        .filter(key => modifiedContributions[key])
        .map(key => modifiedContributions[key]);
    let deletions = deletedContributions;
    let dto = {
        newContributions: additions,
        editedContributions: modifications,
        deletedContributions: deletions
    };
    let response = await postAsync("/api/film/edit-cast", dto);
    if(response.ok) {
        window.location.replace("/film");
    }
}

function readAdditions(list, id) {
    let additions = [];
    for(let i = 0; i < list.children.length; ++i) {
        let c = list.children[i];
        additions.push({
            actor: c.getElementsByClassName("actor")[0].selectedOptions[0].value,
            film: id,
            role: c.getElementsByClassName("role")[0].selectedOptions[0].value
        });
    }
    return additions;
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