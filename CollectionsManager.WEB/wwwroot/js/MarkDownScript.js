{
    const previewBlock = document.getElementById("descriptionPreviewBlock");
    const descInput = document.getElementById("descriptionInput");
    const view = document.getElementById("previewViewBlock");

    const getPreview = () => {
        if (descInput.value.length > 0) {
            previewBlock.style.display = "block";

            let response = fetch("/Collections/GetMarkDownPreview",
                    {
                        method: "POST",
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ value: descInput.value })
                    })
                .then(resp => resp.text())
                .then(result => view.innerHTML = result);
        } else {
            previewBlock.style.display = "none";
        }
    }

    getPreview();

    descInput.onkeyup = () => {
        getPreview();
    }
}