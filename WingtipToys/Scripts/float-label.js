$(document).ready(function() {
	$.support.placeholder = (function() {
		var i = document.createElement('input');
		return 'placeholder' in i;
	})();

	// If placeholders are supported, work with the labels
	if ($.support.placeholder) {
		// Hide labels by default
		$('.form-group.float-label').each(function() {
			$(this).addClass('js-hide-label');
		});
		// Don't hide them for select
		$('.form-group.float-label').find('select').each(function() {
			$(this).parent().removeClass('js-hide-label').addClass('js-unhighlight-label');
		});

		$('.form-group.float-label').find('input, textarea, select').on('keyup blur focus change', function(e) {
			// Cache our selectors
			var $this = $(this),
				$parent = $this.parent();

			// Add or remove classes
			if (e.type == 'keyup') {
				if ($this.val() == '') {
					$parent.addClass('js-hide-label');
				} else {
					$parent.removeClass('js-hide-label');
				}
			} else if (e.type == 'blur') {
				if ($this.val() == '') {
					$parent.addClass('js-hide-label');
				} else {
					$parent.removeClass('js-hide-label').addClass('js-unhighlight-label');
				}
			} else if (e.type == 'change') {
				if ($this.val() == '') {
					$parent.addClass('js-hide-label');
				} else {
					$parent.removeClass('js-hide-label');
				}
			} else if (e.type == 'focus') {
				if ($this.val() != '') {
					$parent.removeClass('js-unhighlight-label');
				}
			}
		});
	}
});