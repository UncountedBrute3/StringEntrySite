document.querySelector('form').addEventListener('submit', function (event) {
    event.preventDefault();

    const dataInput = document.getElementById('data').value;
    const encodedSearchInput = encodeURIComponent(dataInput);

    fetch(`/words?word=${encodedSearchInput}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => renderSuccess(data))
        .catch((error) => renderError(error));
});

const renderSuccess = (result) => {
    if (result.isSuccess) {
        if (result.value.length === 0) {
            renderError({errors: ['No words found.']});
            return;
        }
        const successElement = document.getElementById('result-items');
        successElement.innerHTML = ''; // Clear previous results
        result.value.forEach(item => {
            const li = document.createElement('li');
            li.textContent = item;
            successElement.appendChild(li);
        });
    } else {
        renderError(result);
    }
}

const renderError = (result) => {
    const errorElement = document.getElementById('result-items');
    errorElement.innerHTML = ''; // Clear previous results
    const li = document.createElement('li');
    if (result !== null && result.errors.length > 0) {
        li.textContent = result.errors.join(', ');
    } else {
        li.textContent = "An error occurred while processing your request.";
    }
    errorElement.appendChild(li);
}