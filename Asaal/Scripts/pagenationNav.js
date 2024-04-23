$(document).ready(function () {

    var currentPageNumber = parseInt($("#pagenationNav").attr("data-currentPageNumber"));
    var countOfCollection = parseInt($("#pagenationNav").attr("data-countOfCollection"));
    var elementsInPage = parseInt($("#pagenationNav").attr("data-elementsInPage"));


    $("#pagenationNav").append(`
        <nav aria-label="Page navigation" class="text-center">
            <ul class="pagination pagination-lg">
                <li>
                    <a href="/Questions/index?pageN=${currentPageNumber - 1}" aria-label="Previous" data-pageNumber="${currentPageNumber - 1}">
                        <span aria-hidden="true">&laquo;</span>
                    </a>
                </li>

                <li>
                    <a href="/Questions/index?pageN=${currentPageNumber - 2}" data-pageNumber="${currentPageNumber - 2}">
                        ${currentPageNumber - 2}
                    </a>
                </li>

                <li>
                    <a href="/Questions/index?pageN=${currentPageNumber - 1}" data-pageNumber="${currentPageNumber - 1}">
                        ${currentPageNumber - 1}
                    </a>
                </li>

                <li class="active">
                    <a href="/Questions/index?pageN=${currentPageNumber}" data-pageNumber="${currentPageNumber}">
                        ${currentPageNumber}
                    </a>
                </li>

                <li>
                    <a href="/Questions/index?pageN=${currentPageNumber + 1}" data-pageNumber="${currentPageNumber + 1}">
                        ${currentPageNumber + 1}
                    </a>
                </li>

                <li>
                    <a href="/Questions/index?pageN=${currentPageNumber + 2}" data-pageNumber="${currentPageNumber + 2}">
                        ${currentPageNumber + 2}
                    </a>
                </li>

                <li>
                    <a href="/Questions/index?pageN=${currentPageNumber + 1}" aria-label="Next" data-pageNumber="${currentPageNumber + 1}">
                        <span aria-hidden="true">&raquo;</span>
                    </a>
                </li>
            </ul>
        </nav>

        `);


    // remove numbers from pagenation nav if 
    $("#pagenationNav li a").each(function () {

        var targetPageNumber = $(this).attr("data-pageNumber");
        if (targetPageNumber.includes("-") || targetPageNumber=== "0") {
            $(this).parents("li").remove();
        }

        var numberOfPage = Math.ceil(countOfCollection / elementsInPage);

        if (targetPageNumber > numberOfPage) {
            $(this).parents("li").remove();
        }

    });



});