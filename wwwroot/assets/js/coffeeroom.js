function mdlOpen(mdlId) {
    document.getElementById("backdrop").style.display = "block"
    document.getElementById(mdlId).style.display = "block"
    document.getElementById(mdlId).classList.add("show")
    var modal = document.getElementById(mdlId);
    window.onclick = function (event) {
        if (event.target == modal) {
            mdlClose(mdlId)
        }
    }
}

function mdlClose(mdlId) {
    document.getElementById("backdrop").style.display = "none"
    document.getElementById(mdlId).style.display = "none"
    document.getElementById(mdlId).classList.remove("show")
}
function mdlCloseAll() {
    const openModal = document.querySelector('.modal.show');
    if (openModal) {
        const modalId = openModal.getAttribute('id'); 
        const modal = new bootstrap.Modal(openModal); 
        modal.hide();
        mdlClose(modalId);
    }
}

function livesearch() {

    const searchStat = document.getElementById('search_stat');
    const searchResults = document.getElementById('search_results');
    searchStat.innerHTML = 'Searching <i class="ai-clock mb-1"></i>';
    var ddr = document.getElementById("global_search").value;
    if (ddr.length >= 2) {
        axios.get('/api/livesearch/all/' + ddr)
            .then(response => {
                console.log(response.data);
                var sb = new StringBuilder();
                for (var i = 0; i < response.data.length; i++) {
                    sb.append('<div class="d-flex align-items-center border-bottom pb-4 mb-4 fade-in">');
                    sb.append('<a class="position-relative d-inline-block flex-shrink-0 bg-secondary rounded-1" href="' + response.data[i].url + '">');
                    sb.append('<img src="/assets/images/search_thumbs/' + response.data[i].image + '.svg" width="70" alt="Product" /></a><div class="ps-3">');
                    sb.append('<h4 class="h6 mb-2"><a href="/">' + response.data[i].title + '</a></h4>');
                    sb.append(' <span class="fs-sm text-muted ms-auto">' + response.data[i].description + '</span>');
                    sb.append('</div></div>');
                }
                searchResults.innerHTML = sb.toString();
                   searchStat.innerHTML = 'Search';
            })
            .catch(error => {
                console.error('Error:', error);
                 searchStat.innerHTML = '';
                searchResults.innerHTML = '';
            });
    }
    else {
        searchResults.innerHTML = '';
        searchStat.innerHTML = 'Search';
    }
}

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
    document.getElementById("toastTitle").innerText = title;
    var toastElement = document.getElementById("toastBox");
    var toast = new bootstrap.Toast(toastElement);
    toast.show();
}

