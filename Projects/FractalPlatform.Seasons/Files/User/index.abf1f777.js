new Swiper(".movies-for-the-future__swiper",{navigation:{nextEl:".movies-for-the-future__swiper .movies-swiper-btn-prev",prevEl:".movies-for-the-future__swiper .movies-swiper-btn-next"},keyboard:{enabled:!0,onlyInViewport:!0},slidesPerView:6,spaceBetween:16,loop:!0,autoplay:{delay:5e3,disableOnInteraction:!1},speed:800,autoHeight:!0}),new Swiper(".movies-favorite__swiper",{navigation:{nextEl:".movies-favorite__swiper .movies-swiper-btn-prev",prevEl:".movies-favorite__swiper .movies-swiper-btn-next"},keyboard:{enabled:!0,onlyInViewport:!0},slidesPerView:6,spaceBetween:16,loop:!0,autoplay:{delay:5e3,disableOnInteraction:!1},speed:800,autoHeight:!0}),new Swiper(".movies-for-one-time__swiper",{navigation:{nextEl:".movies-for-one-time__swiper .movies-swiper-btn-prev",prevEl:".movies-for-one-time__swiper .movies-swiper-btn-next"},keyboard:{enabled:!0,onlyInViewport:!0},slidesPerView:6,spaceBetween:16,loop:!0,autoplay:{delay:5e3,disableOnInteraction:!1},speed:800,autoHeight:!0}),document.addEventListener("click",(function(e){if(!e.target.classList.contains("filters-dropdown__item"))return;let i=Array.from(e.target.parentElement.children);e.target.parentElement.classList.contains("isActive")?(i.forEach((e=>{e.classList.add("isHidden")})),e.target.classList.remove("isHidden"),e.target.parentElement.classList.remove("isActive")):(e.target.parentElement.classList.add("isActive"),i.forEach((e=>{e.classList.remove("isHidden")})))}));
//# sourceMappingURL=index.abf1f777.js.map
