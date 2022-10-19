{
    const tagInput = document.getElementById('tagInput');
    const addTagButton = document.getElementById('addTagButton');
    const tagsRow = document.getElementById('tagsRow');
    const addItemForm = document.getElementById('addItemForm');
    const tagsList = document.getElementById('tagsList');
    let addedTags = [];

    addTagButton.onclick = () => {
        const value = tagInput.value;

        if (value && !addedTags.includes(value) && value.length < 50) {
            tagsRow.insertAdjacentHTML('beforeend', value + ", ");
            addItemForm.insertAdjacentHTML('beforeend', `<input type='text' style="display: none;"  name='Tags[${addedTags.length}].name' value='${value}' />`);
            addedTags.push(value);
        }

    };

    tagsList.onclick = (event) => {
        tagsList.style.display = "none";
        tagInput.value = event.target.innerHTML;
    }

    tagInput.onkeyup = async () => {
        tagsList.style.display = "none";
        tagsList.innerHTML = '';

        if (tagInput.value.length > 0) {

            let url = `/tags/findtags/?substring=${tagInput.value}`;

            let response = await fetch(url)
                .then(resp => resp.json());

            if (response.length > 0) {

                response.forEach((item) => {
                    let li = `<li class ="tags-li">${item}</li>`;
                    tagsList.insertAdjacentHTML('beforeend', li);
                });

                tagsList.style.display = "block";

            }
        }

    }

}