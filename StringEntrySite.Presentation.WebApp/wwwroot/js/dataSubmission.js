document.querySelector('form').addEventListener('submit', function (event) {
    event.preventDefault();

    const dataInput = document.getElementById('data').value;

    fetch('/words', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({sentence: dataInput})
    })
        .then(response => response.json())
        .then(data => renderSuccess(data))
        .catch((error) => renderError(error));
});

const renderSuccess = (result) => {
    if (result.isSuccess) {
        const successElement = document.getElementById('results');
        successElement.textContent = `Data submitted successfully!. Word that was accepted was ${result.value}`;
    } else {
        renderError(result);
    }
}

const renderError = (result) => {
    const errorElement = document.getElementById('results');
    if (result !== null) {
        errorElement.textContent = result.errors.join(', ');
    }
    else
    {
        errorElement.textContent = "An error occurred while processing your request.";
    }
}