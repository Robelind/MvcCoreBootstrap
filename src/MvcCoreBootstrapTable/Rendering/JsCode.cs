namespace MvcCoreBootstrapTable.Rendering
{
    internal static class JsCode
    {
        public static string Code =>
        @"
<script type=""text/javascript"">
    $(document).ready(function() {{
        $('#{0} [data-pageselector-id]').change(function () {{
            $($(this).data('pageselector-id')).attr('data-ajax-url', $(this).val());
            $($(this).data('pageselector-id')).click();
        }});

        $('#{0} [data-filter-prop]').on('input', function(e) {{
            if ($(this).val().length == 0 || $(this).val().length >= $(this).data('filter-threshold')) {{
                var queryAttrs = '';

                $('#{0} [data-filter-prop]').each(function () {{
                    if ($(this).val().length > 0) {{
                        queryAttrs += '&filter[]=' + $(this).data('filter-prop') + '&filter[]=' + $(this).val() + '&filter[]=false';
                    }}
                }});
                queryAttrs += '&currentFilter=' + $(this).data('filter-prop');
                $('#{0} #FilterLink').attr('data-ajax-url', $('#{0} #FilterLinkTemplate').val() + queryAttrs);
                $('#{0} #FilterLink').click();
            }}
        }});

        $('#{0} [data-filter-focus]').each(function () {{
            var strLength = $(this).val().length * 2;

            $(this).focus();
            $(this)[0].setSelectionRange(strLength, strLength);
        }});
    }});
</script>
        ";
    }
}
