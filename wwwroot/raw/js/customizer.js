(function () {

  // Get from local storage function
  function getFromStorage(item) {
    return localStorage.getItem(item);
  }

  // Set from local storage function
  function setToStorage(item, value) {
    localStorage.setItem(item, value);
  }

  // Apply generated styles from local storage on page load
  var themePrefix = 'ar-';
  var generatedStylesheet = document.getElementById('customizer-styles');
  var generatedStylesModal = document.getElementById('custom-generated-styles');
  var generatedStylesheetRules = `
    :root {
      ${getFromStorage('theme-primary') && `--${themePrefix}primary: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary-rgb') && `--${themePrefix}primary-rgb: ${getFromStorage('theme-primary-rgb')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}warning-rgb: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning-rgb') && `--${themePrefix}warning-rgb: ${getFromStorage('theme-warning-rgb')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}info-rgb: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info-rgb') && `--${themePrefix}info-rgb: ${getFromStorage('theme-info-rgb')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}success-rgb: ${getFromStorage('success-info')};`}
      ${getFromStorage('theme-success-rgb') && `--${themePrefix}success-rgb: ${getFromStorage('success-info-rgb')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}danger-rgb: ${getFromStorage('danger-info')};`}
      ${getFromStorage('theme-danger-rgb') && `--${themePrefix}danger-rgb: ${getFromStorage('danger-info-rgb')};`}
      ${getFromStorage('theme-font-family') && `--${themePrefix}body-font-family: ${getFromStorage('theme-font-family')};`}
      ${getFromStorage('theme-root-font-size') && `--${themePrefix}root-font-size: ${getFromStorage('theme-root-font-size')};`}
      ${getFromStorage('theme-body-font-size') && `--${themePrefix}body-font-size: ${getFromStorage('theme-body-font-size')};`}
      ${getFromStorage('theme-border-width') && `--${themePrefix}border-width: ${getFromStorage('theme-border-width')};`}
      ${getFromStorage('theme-border-radius') && `--${themePrefix}border-radius: ${getFromStorage('theme-border-radius')};`}
      ${getFromStorage('theme-border-radius-sm') && `--${themePrefix}border-radius-sm: ${getFromStorage('theme-border-radius-sm')};`}
      ${getFromStorage('theme-border-radius-lg') && `--${themePrefix}border-radius-lg: ${getFromStorage('theme-border-radius-lg')};`}
      ${getFromStorage('theme-border-radius-xl') && `--${themePrefix}border-radius-xl: ${getFromStorage('theme-border-radius-xl')};`}
      ${getFromStorage('theme-border-radius-2xl') && `--${themePrefix}border-radius-2xl: ${getFromStorage('theme-border-radius-2xl')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}link-color: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary-btn-hover-active-border-bg-color') && `--${themePrefix}link-hover-color: ${getFromStorage('theme-primary-btn-hover-active-border-bg-color')};`}
    }
    .btn-primary {
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-bg: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-primary-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-primary-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-primary-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-primary-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-primary-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-primary-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-primary-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-disabled-bg: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-primary')};`}
    }
    .btn-warning {
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-bg: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-warning-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-warning-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-warning-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-warning-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-warning-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-warning-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-warning-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-disabled-bg: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-warning')};`}
    }
    .btn-info {
      ${getFromStorage('theme-info') && `--${themePrefix}btn-bg: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-info-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-info-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-info-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-info-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-info-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-info-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-info-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-disabled-bg: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-info')};`}
    }
    .btn-success {
      ${getFromStorage('theme-success') && `--${themePrefix}btn-bg: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-success-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-success-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-success-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-success-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-success-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-success-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-success-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-disabled-bg: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-success')};`}
    }
    .btn-danger {
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-bg: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-danger-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-danger-btn-hover-active-border-bg-color') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-danger-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-danger-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-danger-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-danger-btn-hover-active-border-bg-color') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-danger-btn-hover-active-border-bg-color')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-disabled-bg: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-danger')};`}
    }
    .btn-outline-primary {
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-color: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-disabled-color: ${getFromStorage('theme-primary')};`}
      ${getFromStorage('theme-primary') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-primary')};`}
    }
    .btn-outline-warning {
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-color: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-disabled-color: ${getFromStorage('theme-warning')};`}
      ${getFromStorage('theme-warning') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-warning')};`}
    }
    .btn-outline-info {
      ${getFromStorage('theme-info') && `--${themePrefix}btn-color: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-disabled-color: ${getFromStorage('theme-info')};`}
      ${getFromStorage('theme-info') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-info')};`}
    }
    .btn-outline-success {
      ${getFromStorage('theme-success') && `--${themePrefix}btn-color: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-disabled-color: ${getFromStorage('theme-success')};`}
      ${getFromStorage('theme-success') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-success')};`}
    }
    .btn-outline-danger {
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-color: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-border-color: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-hover-bg: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-hover-border-color: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-active-bg: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-active-border-color: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-disabled-color: ${getFromStorage('theme-danger')};`}
      ${getFromStorage('theme-danger') && `--${themePrefix}btn-disabled-border-color: ${getFromStorage('theme-danger')};`}
    }
  `;
  var generatedStylesheetRulesFormatted = generatedStylesheetRules;
  var generatedStylesheetRulesFormatted = generatedStylesheetRules.replace(/null|\s*(?![A-Z])/g, '');

  generatedStylesheet.append(generatedStylesheetRulesFormatted);
  generatedStylesheetRulesFormatted.split('}').map(function(item) {
    if (item.indexOf('{--') !== -1) {
      generatedStylesModal.append(item + '}');
    }
  });
  
  var styleRules = {
    root: generatedStylesheet.sheet.cssRules[0],
    primaryBtn: generatedStylesheet.sheet.cssRules[1],
    warningBtn: generatedStylesheet.sheet.cssRules[2],
    infoBtn: generatedStylesheet.sheet.cssRules[3],
    successBtn: generatedStylesheet.sheet.cssRules[4],
    dangerBtn: generatedStylesheet.sheet.cssRules[5],
    primaryOutlineBtn: generatedStylesheet.sheet.cssRules[6],
    warningOutlineBtn: generatedStylesheet.sheet.cssRules[7],
    infoOutlineBtn: generatedStylesheet.sheet.cssRules[8],
    successOutlineBtn: generatedStylesheet.sheet.cssRules[9],
    dangerOutlineBtn: generatedStylesheet.sheet.cssRules[10]
  };

  // Add / update generated styles to "Show styles" modal on control change
  function customStylesModal() {
    generatedStylesModal.innerHTML = styleRules.root.cssText.replace(/null|\s*(?![A-Z])/g,'');
    generatedStylesModal.innerHTML += styleRules.primaryBtn.cssText.indexOf('{ }') === -1 ? styleRules.primaryBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.warningBtn.cssText.indexOf('{ }') === -1 ? styleRules.warningBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.infoBtn.cssText.indexOf('{ }') === -1 ? styleRules.infoBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.successBtn.cssText.indexOf('{ }') === -1 ? styleRules.successBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.dangerBtn.cssText.indexOf('{ }') === -1 ? styleRules.dangerBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.primaryOutlineBtn.cssText.indexOf('{ }') === -1 ? styleRules.primaryOutlineBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.warningOutlineBtn.cssText.indexOf('{ }') === -1 ? styleRules.warningOutlineBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.infoOutlineBtn.cssText.indexOf('{ }') === -1 ? styleRules.infoOutlineBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.successOutlineBtn.cssText.indexOf('{ }') === -1 ? styleRules.successOutlineBtn.cssText.replace(/ /g,'') : '';
    generatedStylesModal.innerHTML += styleRules.dangerOutlineBtn.cssText.indexOf('{ }') === -1 ? styleRules.dangerOutlineBtn.cssText.replace(/ /g,'') : '';
  };


  // Convert HEX to RGB colors
  function hexToRGB(h) {
    var r = 0, g = 0, b = 0;
    if (h.length == 4) {
      r = "0x" + h[1] + h[1];
      g = "0x" + h[2] + h[2];
      b = "0x" + h[3] + h[3];
    } else if (h.length == 7) {
      r = "0x" + h[1] + h[2];
      g = "0x" + h[3] + h[4];
      b = "0x" + h[5] + h[6];
    }
    return +r + "," + +g + "," + +b;
  }

  // Shade color Lighten / Darken function
  function shadeColor(color, percent) {
    var num = parseInt(color.replace('#',''),16),
    amt = Math.round(2.55 * percent),
    R = (num >> 16) + amt,
    B = (num >> 8 & 0x00FF) + amt,
    G = (num & 0x0000FF) + amt;
    return '#' + (0x1000000 + (R<255?R<1?0:R:255)*0x10000 + (B<255?B<1?0:B:255)*0x100 + (G<255?G<1?0:G:255)).toString(16).slice(1);
  }


  // Colors
  var themeColors = document.querySelectorAll('#theme-colors .color-swatch');
  
  for (var i = 0; i < themeColors.length; i++) {
    var colorPreview = themeColors[i].querySelector('label'),
        colorPicker = themeColors[i].querySelector('input[type="color"]')
        colorInput = themeColors[i].querySelector('input[type="text"]');

    colorInput.value = getFromStorage(themeColors[i].id) || colorInput.value;
    colorPicker.value = getFromStorage(themeColors[i].id) || colorPicker.value;
    colorPreview.style.backgroundColor = getFromStorage(themeColors[i].id) || colorPicker.value;

    // Color picker change event
    colorPicker.addEventListener('input', function(e) {
      var colorPreview = e.target.parentElement.querySelector('label'),
          colorInput = e.target.parentElement.querySelector('input[type="text"]'),
          colorValue = e.target.value,
          btnStyles = generatedStylesheet.sheet.cssRules[i + 1]

      setToStorage('theme-' + e.target.id, colorValue);
      setToStorage('theme-' + e.target.id + '-rgb', hexToRGB(colorValue));
      setToStorage('theme-' + e.target.id + '-btn-hover-active-border-bg-color', shadeColor(colorValue, -10));
      colorPreview.style.backgroundColor = colorValue;
      colorInput.value = colorValue;
      styleRules.root.style.setProperty('--' + themePrefix + e.target.id, colorValue);
      styleRules.root.style.setProperty('--' + themePrefix + e.target.id + '-rgb', hexToRGB(colorValue));
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-bg', colorValue);
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-border-color', colorValue);
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-hover-bg', shadeColor(colorValue, -10));
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-hover-border-color', shadeColor(colorValue, -10));
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-active-bg', shadeColor(colorValue, -10));
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-active-border-color', shadeColor(colorValue, -10));
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-disabled-bg', colorValue);
      styleRules[e.target.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-disabled-border-color', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-color', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-border-color', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-hover-bg', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-hover-border-color', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-active-bg', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-active-border-color', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-disabled-color', colorValue);
      styleRules[e.target.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-disabled-border-color', colorValue);

      if (e.target.id === 'primary') {
        setToStorage('theme-link-hover-color', shadeColor(colorValue, -10));
        styleRules.root.style.setProperty('--' + themePrefix + 'link-color', colorValue);
        styleRules.root.style.setProperty('--' + themePrefix + 'link-hover-color', shadeColor(colorValue, -10));
      }

      customStylesModal();
    });

    // Color input change event
    colorInput.addEventListener('change', function(e) {
      var colorPreview = e.target.parentElement.querySelector('label'),
          colorPicker = e.target.parentElement.querySelector('input[type="color"]'),
          colorValue = e.target.value;
      if(colorValue.length != 0 && colorValue.startsWith('#')) {
        setToStorage('theme-' + colorPicker.id, colorValue);
        setToStorage('theme-' + colorPicker.id + '-rgb', hexToRGB(colorValue));
        setToStorage('theme-' + colorPicker.id + '-btn-hover-active-border-bg-color', shadeColor(colorValue, -10));
        colorPreview.style.backgroundColor = colorValue;
        colorPicker.value = colorValue;
        styleRules.root.style.setProperty('--' + themePrefix + colorPicker.id, colorValue);
        styleRules.root.style.setProperty('--' + themePrefix + colorPicker.id + '-rgb', hexToRGB(colorValue));
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-bg', colorValue);
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-border-color', colorValue);
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-hover-bg', shadeColor(colorValue, -10));
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-hover-border-color', shadeColor(colorValue, -10));
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-active-bg', shadeColor(colorValue, -10));
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-active-border-color', shadeColor(colorValue, -10));
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-disabled-bg', colorValue);
        styleRules[colorPicker.id + 'Btn'].style.setProperty('--' + themePrefix + 'btn-disabled-border-color', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-color', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-border-color', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-hover-bg', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-hover-border-color', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-active-bg', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-active-border-color', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-disabled-color', colorValue);
        styleRules[colorPicker.id + 'OutlineBtn'].style.setProperty('--' + themePrefix + 'btn-disabled-border-color', colorValue);

        if (colorPicker.id === 'primary') {
          setToStorage('theme-link-hover-color', shadeColor(colorValue, -10));
          styleRules.root.style.setProperty('--' + themePrefix + 'link-color', colorValue);
          styleRules.root.style.setProperty('--' + themePrefix + 'link-hover-color', shadeColor(colorValue, -10));
        }

        customStylesModal();
      }
    });
  }


  // Typography

  // Font URL
  var fontURLInput = document.getElementById('font-url'),
      fontURLLink = document.getElementById('google-font');
  fontURLLink.href = getFromStorage('theme-font-url') || fontURLInput.value;
  fontURLInput.value = getFromStorage('theme-font-url') || fontURLInput.value;
  fontURLInput.addEventListener('blur', function(e) {
    var fontURL = e.target.value;
    setToStorage('theme-font-url', fontURL);
    fontURLLink.href = fontURL;
  });

  // Font family
  var fontFamilyInput = document.getElementById('body-font-family');
  fontFamilyInput.value =  getFromStorage('theme-font-family') || fontFamilyInput.value;
  fontFamilyInput.addEventListener('blur', function(e) {
    var fontFamily = e.target.value;
    setToStorage('theme-font-family', fontFamily);
    styleRules.root.style.setProperty('--' + themePrefix + 'body-font-family', fontFamily);
    customStylesModal();
  });

  // Root font size
  var rootFontSizeSelect = document.getElementById('root-font-size');
  rootFontSizeSelect.value = getFromStorage('theme-root-font-size') || rootFontSizeSelect.value;
  rootFontSizeSelect.addEventListener('change', function(e) {
    var rootFontSize = e.target.value;
    setToStorage('theme-root-font-size', rootFontSize);
    styleRules.root.style.setProperty('--' + themePrefix + 'root-font-size', rootFontSize);
    customStylesModal();
  });


  // Body font size
  var bodyFontSizeSelect = document.getElementById('body-font-size');
  bodyFontSizeSelect.value = getFromStorage('theme-body-font-size') || bodyFontSizeSelect.value;
  bodyFontSizeSelect.addEventListener('change', function(e) {
    var bodyFontSize = e.target.value;
    setToStorage('theme-body-font-size', bodyFontSize);
    styleRules.root.style.setProperty('--' + themePrefix + 'body-font-size', bodyFontSize);
    customStylesModal();
  });

  
  // Borders width
  var borderWidthInput = document.getElementById('border-width');
  borderWidthInput.value = parseInt(getFromStorage('theme-border-width')) || borderWidthInput.value;
  borderWidthInput.addEventListener('input', function(e) {
    var borderWidth = e.target.value;
    setToStorage('theme-border-width', borderWidth + 'px');
    styleRules.root.style.setProperty('--' + themePrefix + 'border-width', borderWidth + 'px');
    customStylesModal();
  });


  // Border radius base
  var borderRadiusInput = document.getElementById('border-radius');
  borderRadiusInput.value = parseFloat(getFromStorage('theme-border-radius')) || borderRadiusInput.value;
  borderRadiusInput.addEventListener('input', function(e) {
    var borderRadius = e.target.value;
    setToStorage('theme-border-radius', borderRadius + 'rem');
    styleRules.root.style.setProperty('--' + themePrefix + 'border-radius', borderRadius + 'rem');
    customStylesModal();
  });

  // Border radius variants
  var themeRadiuses = document.querySelectorAll('#theme-border-radiuses input[type="number"');
  for (var i = 0; i < themeRadiuses.length; i++) {
    themeRadiuses[i].value = getFromStorage('theme-' + themeRadiuses[i].id + 'multiplier') || themeRadiuses[i].value;
    themeRadiuses[i].addEventListener('input', function(e) {
      var radiusMultiplier = e.target.value;
      setToStorage('theme-' + e.target.id, 'calc(var(--' + themePrefix + 'border-radius) * ' + radiusMultiplier + ')');
      setToStorage('theme-' + e.target.id + 'multiplier', radiusMultiplier);
      styleRules.root.style.setProperty('--' + themePrefix + e.target.id, 'calc(var(--' + themePrefix + 'border-radius) * ' + radiusMultiplier + ')');
      customStylesModal();
    });
  }

  
  // Show action buttons when any setting is changed
  var customizer = document.getElementById('customizer'),
      actionBtns = document.getElementById('customizer-btns');
  if (getFromStorage('theme-customizer-btns')) {
    actionBtns.classList.remove('d-none');
  }
  customizer.addEventListener('change', function(e) {
    setToStorage('theme-customizer-btns', true);
    actionBtns.classList.remove('d-none');
  });


  // Reset settings
  var resetBtn = document.getElementById('customizer-reset-btn');
  resetBtn.addEventListener('click', function() {
    var storageKeys = [];
    for (var i = 0; i < localStorage.length; i++){
      if (localStorage.key(i).substring(0,5) == 'theme') {
        storageKeys.push(localStorage.key(i));
      }
    }
    for (var i = 0; i < storageKeys.length; i++) {
      localStorage.removeItem(storageKeys[i]);
    }
    window.location.reload();
  });


  // Copy styles to clipboard
  var copyStylesBtn = document.getElementById('copy-styles-btn');
  copyStylesBtn.addEventListener('click', function() {
    var range = document.createRange();
    range.selectNode(generatedStylesModal);
    window.getSelection().removeAllRanges();
    window.getSelection().addRange(range);
    navigator.clipboard.writeText(generatedStylesModal.innerHTML);
    this.innerHTML = '<i class="ai-check me-2 ms-n1"></i>Copied!'
    setTimeout(function() {
      copyStylesBtn.innerHTML = '<i class="ai-copy me-2 ms-n1"></i>Copy styles'
    }, 2000)
  });
})();
