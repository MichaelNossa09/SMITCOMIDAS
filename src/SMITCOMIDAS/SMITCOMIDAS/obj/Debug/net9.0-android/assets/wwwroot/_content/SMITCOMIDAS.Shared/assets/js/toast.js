// Función simple para mostrar notificaciones toast
window.showToast = (message, type) => {
    // Crear el toast
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    toast.textContent = message;

    // Agregar el toast al contenedor
    const container = document.getElementById('toast-container') || createToastContainer();
    container.appendChild(toast);

    // Eliminar después de 3 segundos
    setTimeout(() => {
        toast.classList.add('toast-fade-out');
        setTimeout(() => {
            container.removeChild(toast);
            if (container.childNodes.length === 0) {
                document.body.removeChild(container);
            }
        }, 300);
    }, 3000);
}

// Crear el contenedor de toasts si no existe
function createToastContainer() {
    const container = document.createElement('div');
    container.id = 'toast-container';
    document.body.appendChild(container);
    return container;
}