 {
    const dateOptions = {weekday: "short", year: "numeric", month: "short", day: "numeric"}
    const commentUrl = `/Comments/GetCommentsForItem/?itemId=${document.getElementById("itemId").value}`;
    const commentsContainer = document.getElementById("commentsContainer");
    const commentsBlock = document.getElementById("commentsBlock");
    let currentCommentsCount = 0;

    const getHtml = (comment) => {
        let date = new Date(comment.date);

        return "<div class='card-body p-4'>" +
        '<div class="d-flex flex-start">' +
        '<div>' +
        `<h6 class="fw-bold mb-1">${comment.authorName}</h6>` +
        '<div class="d-flex align-items-center mb-3">' +
        `<p class="mb-0">${date.toLocaleDateString("en-US", dateOptions)} ${date.toLocaleTimeString("en-US")}</p>` +
        '</div>' +
        `<p class="mb-0">${comment.text}</p>`+
        '</div></div></div>';
    }

    const getComments = async () => {
        let response = await fetch(commentUrl)
            .then(resp => resp.json());
        return response;
    }

    const updateComments = async () =>{
        let comments = await getComments();
        let commentsToAdd = comments.slice(currentCommentsCount, comments.length + 1);
        currentCommentsCount = comments.length;

        if (currentCommentsCount > 0) {
            commentsContainer.style.display = "block";
        }

        commentsToAdd.forEach(elem => {
            commentsBlock.insertAdjacentHTML("afterbegin", getHtml(elem))
        });
    }

    updateComments();
    setInterval(() => updateComments(), 5000);
 }