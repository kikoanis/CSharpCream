<h3>Subscribe to our<br />newsletter</h3>
<?php
	$hasError = false;
	$nameError = false;
	$emailError = false;
	
	if($_POST['name'] == ""){
		$hasError = true;
		$nameError = true;
	}
		
	if($_POST['email'] == ""){
		$hasError = true;
		$emailError = true;
	}
		
	if(!eregi('^[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.([a-zA-Z]{2,4})$', $_POST['email'])){
		$hasError = true;
		$emailError = true;
	}
		
	if($hasError){ ?>
	<form name="ajaxForm" action="ajax/form_submit.php" class="genericForm">
		<label for="name">Name</label>
		<input type="text" name="name" id="name" value="<?php echo $_POST['name'] ?>" />
		<?php if($nameError){ ?><div class="error-message">Please enter your name.</div><?php } ?>
	
		<label for="email">Email</label>
		<input type="text" name="email" id="email" value="<?php echo $_POST['email'] ?>" />
		<?php if($emailError){ ?><div class="error-message">Please enter a valid email.</div><?php } ?>
	
		<input type="submit" value="Submit" class="submit" />
	</form>
	<?php }else{ ?>
		<p><small>Thank you, the form has been successfully submitted!</small></p>
	<?php } ?>