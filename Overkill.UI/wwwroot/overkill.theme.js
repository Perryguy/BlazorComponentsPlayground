export function getSystemMode() {
    try {
        return matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    } catch { return 'light'; }
}

export function subscribeSystemMode(dotnet) {
    try {
        const mq = matchMedia('(prefers-color-scheme: dark)');
        const handler = (e) => dotnet.invokeMethodAsync('Overkill_OnSystemModeChanged', e.matches ? 'dark' : 'light');
        if (mq.addEventListener) mq.addEventListener('change', handler);
        else mq.addListener(handler);
    } catch { /* no-op */ }
}
