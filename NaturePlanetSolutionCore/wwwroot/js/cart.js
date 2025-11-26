document.addEventListener('DOMContentLoaded', function() {
    // Handle remove from cart form submissions
    document.addEventListener('click', async function(e) {
        const removeButton = e.target.closest('.remove-button');
        if (!removeButton) return;
        
        e.preventDefault();
        
        const form = removeButton.closest('form.remove-item-form');
        if (!form) return;
        
        const formData = new FormData(form);
        const token = form.querySelector('input[name="__RequestVerificationToken"]').value;
        
        try {
            const response = await fetch(form.action, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': token
                }
            });

            if (response.ok) {
                // Reload the cart content
                const cartResponse = await fetch(window.location.href);
                const html = await cartResponse.text();
                const tempDiv = document.createElement('div');
                tempDiv.innerHTML = html;
                const newCartContent = tempDiv.querySelector('#cartContent').innerHTML;
                document.getElementById('cartContent').innerHTML = newCartContent;
            } else {
                console.error('Failed to remove item from cart');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    });
});
