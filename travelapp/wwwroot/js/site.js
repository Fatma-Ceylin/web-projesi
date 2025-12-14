

document.addEventListener("DOMContentLoaded", function () {
   
    const heroImage = document.querySelector('.hero-image.reveal-right-onload');
    const heroText = document.querySelector('.hero-text');

    if (heroImage) {
        if (heroImage.complete) {
            revealHero();
        } else {
            heroImage.addEventListener('load', revealHero);
        }
    }

    function revealHero() {
        heroImage.classList.add('visible');
        setTimeout(() => {
            if (heroText) heroText.classList.add('visible');
        }, 300);
    }

    const options = {
        root: null,
        rootMargin: '0px',
        threshold: 0.15
    };

    const observer = new IntersectionObserver((entries, obs) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('visible');
                obs.unobserve(entry.target);
            }
        });
    }, options);

    document.querySelectorAll('.reveal-on-scroll-left, .reveal-on-scroll-right, .feature-text').forEach(el => {
        observer.observe(el);
    });
});



//rating with stars
$(document).ready(function() {
    const $stars = $('.rating-stars .star');
    const $ratingInput = $('#RatingValue');


    $stars.on('mouseenter', function() {
        const index = $(this).data('index'); 
        $stars.removeClass('hover');
        $stars.each(function() {
            if ($(this).data('index') <= index) {
                $(this).addClass('hover');
            }
        });
    }).on('mouseleave', function() {
        $stars.removeClass('hover');
    });


    $stars.on('click', function() {
        const index = $(this).data('index'); 
        const rating = index + 1; 
        
        $ratingInput.val(rating); 

        $stars.removeClass('selected');
        
        $stars.each(function() {
            if ($(this).data('index') <= index) {
                $(this).addClass('selected');
            }
        });
    });
});