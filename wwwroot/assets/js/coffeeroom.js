

const lnkDisableElements = document.querySelectorAll('.lnk_disable');

// Iterate through the elements and add a click event listener to each one
lnkDisableElements.forEach(element => {
    element.addEventListener('click', (event) => {
        // Prevent the default behavior of the click event, which is opening the link
        event.preventDefault();
        // Optionally, you can add some other actions here, like showing a message or performing a different action
    });
});

const shareButton = document.getElementById('shareButton');
if (shareButton != null && shareButton != undefined) {
    shareButton.addEventListener('click', async () => {
        try {
            if (navigator.share) {
                await navigator.share({
                    title: 'Check out this amazing post @ thecoffeeroom.in',
                    url: window.location.href,
                });
                console.log('Successfully shared');
            } else if (navigator.clipboard && navigator.clipboard.writeText) {
                await navigator.clipboard.writeText(window.location.href);
                console.log('URL copied to clipboard');
            } else {
                console.log('Sharing not supported');
            }
        } catch (error) {
            console.log('Error sharing:', error);
        }
    });
}

class StringBuilder {
    constructor() {
        this.buffer = "";
    }

    append(string) {
        this.buffer += string;
    }

    toString() {
        return this.buffer;
    }
}

function load_images_temp() {
    const elements = document.querySelectorAll('.img_placeholder');
    const isHD = localStorage.getItem('hd') === 'true';

    elements.forEach(element => {
        const originalString = element.getAttribute('src');
        let modifiedString = originalString.replace(new RegExp('_placeholder', 'g'), '');

        if (isHD) {
            modifiedString = modifiedString.replace(new RegExp('img_', 'g'), 'img_hd');
        }

        element.setAttribute('src', modifiedString);
    });
}

function toaster(title, message) {
    //removed trim from message and title
    document.getElementById("toastBody").innerText = message;
    document.getElementById("toastTitle").innerText = title
    var toastElement = document.getElementById("toastBox");
    var toast = new bootstrap.Toast(toastElement);
    toast.show();
}
