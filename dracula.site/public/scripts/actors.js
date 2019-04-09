async function actorSelected(id) {
    console.log(`Selected ${id}`);
    let result = await fetch(`/actor/${id}`);
    let page = await result.text();
    let details = document.getElementById('details');
    details.innerHTML = page;
}