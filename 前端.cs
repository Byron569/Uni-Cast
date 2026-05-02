<!DOCTYPE html>
<html lang="zh-CN">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Nexus Cast</title>
<style>
/* ═══════════════════════════════════════════════════════════════════
   Nexus Cast — Vision Pro Glassmorphism Design System
   KOOK indigo + CS2 cyan hybrid
   ═══════════════════════════════════════════════════════════════════ */

:root {
  --glass-bg: rgba(255,255,255,0.05);
  --glass-bg-hover: rgba(255,255,255,0.08);
  --glass-bg-active: rgba(255,255,255,0.12);
  --glass-bg-elevated: rgba(255,255,255,0.07);
  --glass-border: rgba(255,255,255,0.06);
  --glass-border-hover: rgba(255,255,255,0.12);
  --glass-border-active: rgba(99,102,241,0.40);
  --glow-indigo: rgba(99,102,241,0.15);
  --glow-indigo-strong: rgba(99,102,241,0.30);
  --glow-cyan: rgba(6,182,212,0.12);
  --shadow-glass: 0 4px 24px rgba(0,0,0,0.30), inset 0 1px 0 rgba(255,255,255,0.05);
  --shadow-glass-elevated: 0 8px 40px rgba(0,0,0,0.40), inset 0 1px 0 rgba(255,255,255,0.08);
  --shadow-glass-glow: 0 8px 40px rgba(0,0,0,0.40), 0 0 60px var(--glow-indigo);
  --radius-sm: 12px;
  --radius-md: 20px;
  --radius-lg: 28px;
  --radius-pill: 9999px;
  --blur-glass: blur(12px);
  --blur-heavy: blur(20px);
  --ease-spring: cubic-bezier(0.34, 1.56, 0.64, 1);
  --ease-smooth: cubic-bezier(0.16, 1, 0.3, 1);
  --ease-out: cubic-bezier(0, 0, 0.2, 1);
  --duration-fast: 150ms;
  --duration-normal: 300ms;
  --duration-slow: 500ms;
  --accent: #6366f1;
  --accent-hover: #818cf8;
  --accent-press: #4f46e5;
  --accent-cyan: #06b6d4;
  --text: #f4f4f5;
  --muted: #71717a;
  --success: #34d399;
  --error: #f87171;
  --bg-deep: #09090b;
  --surface: rgba(255,255,255,0.02);
  --input-bg: rgba(0,0,0,0.30);
}

* { margin: 0; padding: 0; box-sizing: border-box; }

body {
  background: var(--bg-deep);
  color: var(--text);
  font-family: 'Segoe UI', system-ui, -apple-system, sans-serif;
  overflow: hidden;
  height: 100vh;
  -webkit-app-region: no-drag;
  user-select: none;
  -webkit-user-select: none;
}

/* animated bg gradient */
body::before {
  content: '';
  position: fixed;
  top: -50%; left: -50%;
  width: 200%; height: 200%;
  background: radial-gradient(ellipse at 30% 20%, rgba(99,102,241,0.06) 0%, transparent 50%),
              radial-gradient(ellipse at 70% 60%, rgba(6,182,212,0.04) 0%, transparent 50%),
              radial-gradient(ellipse at 50% 80%, rgba(99,102,241,0.03) 0%, transparent 40%);
  animation: bg-drift 20s ease-in-out infinite;
  z-index: 0;
  pointer-events: none;
}
@keyframes bg-drift {
  0%, 100% { transform: translate(0, 0); }
  33% { transform: translate(1%, -1%); }
  66% { transform: translate(-1%, 1%); }
}

/* scrollbar */
::-webkit-scrollbar { width: 4px; }
::-webkit-scrollbar-track { background: transparent; }
::-webkit-scrollbar-thumb { background: rgba(255,255,255,0.08); border-radius: 4px; }
::-webkit-scrollbar-thumb:hover { background: rgba(255,255,255,0.15); }

/* ═══════════════════════════════════════════════════════════════════
   LAYOUT
   ═══════════════════════════════════════════════════════════════════ */

#app {
  position: relative;
  z-index: 1;
  height: 100vh;
  display: flex;
  flex-direction: column;
}

/* Title Bar */
.titlebar {
  display: flex;
  align-items: center;
  padding: 0 20px;
  height: 44px;
  min-height: 44px;
  background: rgba(13,13,18,0.95);
  backdrop-filter: var(--blur-glass);
  -webkit-backdrop-filter: var(--blur-glass);
  border-bottom: 0.5px solid var(--glass-border);
  -webkit-app-region: drag;
  z-index: 100;
}
.titlebar-logo {
  font-size: 15px;
  font-weight: 700;
  letter-spacing: -0.01em;
  color: #fff;
  display: flex;
  align-items: center;
  gap: 8px;
}
.titlebar-dot {
  width: 8px; height: 8px;
  border-radius: 50%;
  background: var(--accent);
  box-shadow: 0 0 10px var(--accent);
  animation: dot-pulse 2s ease-in-out infinite;
}
@keyframes dot-pulse {
  0%, 100% { box-shadow: 0 0 8px var(--accent); }
  50% { box-shadow: 0 0 18px var(--accent), 0 0 30px rgba(99,102,241,0.4); }
}
.titlebar-ver {
  font-size: 10px;
  color: var(--muted);
  margin-left: 4px;
  font-weight: 400;
}
.titlebar-actions {
  margin-left: auto;
  display: flex;
  gap: 6px;
  -webkit-app-region: no-drag;
}
.tb-btn {
  width: 28px; height: 28px;
  display: flex; align-items: center; justify-content: center;
  border-radius: 6px;
  cursor: pointer;
  color: var(--muted);
  font-size: 16px;
  transition: all var(--duration-fast) var(--ease-smooth);
  background: transparent;
  border: none;
  font-family: inherit;
}
.tb-btn:hover { background: rgba(255,255,255,0.08); color: #fff; }
.tb-btn.close:hover { background: #ef4444; color: #fff; }

/* Main Content */
.content {
  flex: 1;
  display: flex;
  gap: 16px;
  padding: 16px;
  min-height: 0;
}

/* Cards */
.card {
  background: var(--glass-bg);
  backdrop-filter: var(--blur-glass);
  -webkit-backdrop-filter: var(--blur-glass);
  border: 0.5px solid var(--glass-border);
  border-radius: var(--radius-md);
  padding: 24px;
  box-shadow: var(--shadow-glass);
  transition: all var(--duration-normal) var(--ease-smooth);
}
.card-left {
  width: 480px;
  min-width: 480px;
  display: flex;
  flex-direction: column;
  gap: 20px;
  animation: card-enter 0.6s var(--ease-smooth) forwards;
}
.card-right {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 16px;
  text-align: center;
  animation: card-enter 0.6s var(--ease-smooth) 0.1s forwards;
  opacity: 0;
}
@keyframes card-enter {
  from { opacity: 0; transform: translateY(16px) scale(0.97); filter: blur(4px); }
  to { opacity: 1; transform: translateY(0) scale(1); filter: blur(0); }
}

.card-header { margin-bottom: 4px; }
.card-title {
  font-size: 18px; font-weight: 700;
  letter-spacing: -0.01em; color: #fff;
}
.card-subtitle {
  font-size: 12px; color: var(--muted);
  margin-top: 2px;
}

/* Section label */
.sec-label {
  font-size: 10px; font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.06em;
  color: var(--muted);
  margin-bottom: 6px;
}

/* ═══════════════════════════════════════════════════════════════════
   FORM CONTROLS
   ═══════════════════════════════════════════════════════════════════ */

/* Segmented control */
.segmented {
  display: flex;
  background: var(--input-bg);
  border-radius: var(--radius-sm);
  padding: 3px;
  border: 0.5px solid var(--glass-border);
}
.seg-option {
  flex: 1;
  padding: 10px 16px;
  text-align: center;
  font-size: 13px;
  font-weight: 500;
  color: var(--muted);
  border-radius: 10px;
  cursor: pointer;
  transition: all var(--duration-normal) var(--ease-smooth);
  background: transparent;
  border: none;
  font-family: inherit;
  white-space: nowrap;
}
.seg-option:hover { color: var(--text); }
.seg-option.active {
  background: rgba(99,102,241,0.15);
  color: #fff;
  box-shadow: 0 2px 8px rgba(0,0,0,0.3);
}

/* IP input row */
.ip-row {
  display: flex;
  gap: 8px;
}
.ip-input {
  flex: 1;
  padding: 11px 14px;
  background: var(--input-bg);
  border: 0.5px solid var(--glass-border);
  border-radius: 10px;
  color: var(--text);
  font-family: 'Cascadia Code', 'Fira Code', 'Consolas', monospace;
  font-size: 14px;
  outline: none;
  transition: all var(--duration-normal) var(--ease-smooth);
}
.ip-input:focus {
  border-color: var(--glass-border-active);
  box-shadow: 0 0 0 3px rgba(99,102,241,0.08);
}
.ip-input:disabled {
  opacity: 0.35;
  pointer-events: none;
}
.ip-detect {
  padding: 10px 14px;
  background: rgba(99,102,241,0.15);
  border: 0.5px solid rgba(99,102,241,0.25);
  border-radius: 10px;
  color: #a5b4fc;
  font-size: 12px;
  font-weight: 600;
  cursor: pointer;
  transition: all var(--duration-normal) var(--ease-smooth);
  font-family: inherit;
  white-space: nowrap;
}
.ip-detect:hover {
  background: rgba(99,102,241,0.25);
  border-color: rgba(99,102,241,0.4);
}
.ip-detect:active { transform: scale(0.96); }
.ip-detect:disabled { opacity: 0.35; pointer-events: none; }

/* Select */
.q-select {
  width: 100%;
  padding: 11px 14px;
  background: var(--input-bg);
  border: 0.5px solid var(--glass-border);
  border-radius: 10px;
  color: var(--text);
  font-size: 13px;
  font-family: inherit;
  outline: none;
  cursor: pointer;
  transition: all var(--duration-normal) var(--ease-smooth);
  appearance: none;
  -webkit-appearance: none;
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' viewBox='0 0 24 24' fill='none' stroke='%2371717a' stroke-width='2'%3E%3Cpath d='M6 9l6 6 6-6'%3E%3C/path%3E%3C/svg%3E");
  background-repeat: no-repeat;
  background-position: right 14px center;
  padding-right: 36px;
}
.q-select:focus {
  border-color: var(--glass-border-active);
  box-shadow: 0 0 0 3px rgba(99,102,241,0.08);
}
.q-select option {
  background: #18181b;
  color: var(--text);
}

/* Primary button */
.btn-connect {
  width: 100%;
  padding: 14px;
  background: linear-gradient(135deg, var(--accent), #7c3aed);
  border: none;
  border-radius: 12px;
  color: #fff;
  font-size: 15px;
  font-weight: 700;
  cursor: pointer;
  transition: all var(--duration-normal) var(--ease-smooth);
  font-family: inherit;
  letter-spacing: 0.01em;
  box-shadow: 0 4px 20px rgba(99,102,241,0.3);
  position: relative;
  overflow: hidden;
}
.btn-connect::after {
  content: '';
  position: absolute;
  inset: 0;
  background: linear-gradient(135deg, transparent 40%, rgba(255,255,255,0.1) 50%, transparent 60%);
  transform: translateX(-100%);
  transition: transform 0.6s;
}
.btn-connect:hover::after { transform: translateX(100%); }
.btn-connect:hover {
  transform: translateY(-1px);
  box-shadow: 0 6px 30px rgba(99,102,241,0.45);
}
.btn-connect:active { transform: scale(0.97); }
.btn-connect:disabled {
  opacity: 0.4;
  cursor: not-allowed;
  pointer-events: none;
}

/* Status bar */
.status-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
}
.status-text {
  font-size: 12px;
  color: var(--muted);
  flex: 1;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
.status-link {
  font-size: 12px;
  color: #a5b4fc;
  cursor: pointer;
  transition: color var(--duration-fast);
  text-decoration: none;
  white-space: nowrap;
  background: none;
  border: none;
  font-family: inherit;
}
.status-link:hover { color: #fff; text-decoration: underline; }

/* ═══════════════════════════════════════════════════════════════════
   RIGHT CARD — Device Info
   ═══════════════════════════════════════════════════════════════════ */

.device-icon-wrap {
  width: 80px; height: 80px;
  border-radius: 50%;
  background: rgba(255,255,255,0.03);
  border: 0.5px solid var(--glass-border-hover);
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: var(--shadow-glass);
}
.device-icon {
  font-size: 32px;
  opacity: 0.5;
}
.device-name {
  font-size: 16px;
  font-weight: 700;
  color: #fff;
}
.device-sub {
  font-size: 12px;
  color: var(--muted);
  line-height: 1.6;
  max-width: 200px;
}

/* Status indicator */
.status-dot {
  display: inline-block;
  width: 6px; height: 6px;
  border-radius: 50%;
  background: var(--muted);
  margin-right: 6px;
  vertical-align: middle;
  transition: background var(--duration-normal);
}
.status-dot.ok { background: var(--success); box-shadow: 0 0 8px rgba(52,211,153,0.5); }
.status-dot.err { background: var(--error); box-shadow: 0 0 8px rgba(248,113,113,0.5); }
.status-dot.busy { background: var(--accent-cyan); box-shadow: 0 0 8px rgba(6,182,212,0.5); animation: dot-pulse 1s ease-in-out infinite; }

/* ═══════════════════════════════════════════════════════════════════
   MODALS
   ═══════════════════════════════════════════════════════════════════ */

.modal-overlay {
  position: fixed;
  inset: 0;
  background: rgba(0,0,0,0.6);
  backdrop-filter: blur(4px);
  -webkit-backdrop-filter: blur(4px);
  z-index: 200;
  display: flex;
  align-items: center;
  justify-content: center;
  animation: overlay-in 0.25s ease-out;
}
@keyframes overlay-in {
  from { opacity: 0; }
  to { opacity: 1; }
}

.modal {
  background: rgba(22,22,32,0.95);
  backdrop-filter: var(--blur-heavy);
  -webkit-backdrop-filter: var(--blur-heavy);
  border: 0.5px solid var(--glass-border-hover);
  border-radius: var(--radius-lg);
  box-shadow: var(--shadow-glass-elevated), 0 0 80px rgba(99,102,241,0.08);
  animation: modal-in 0.4s var(--ease-smooth) forwards;
  max-height: 85vh;
  overflow-y: auto;
}
@keyframes modal-in {
  from { opacity: 0; transform: scale(0.92) translateY(20px); filter: blur(4px); }
  to { opacity: 1; transform: scale(1) translateY(0); filter: blur(0); }
}

.modal-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px 24px 16px;
  border-bottom: 0.5px solid var(--glass-border);
}
.modal-title {
  font-size: 16px;
  font-weight: 700;
  color: #fff;
}
.modal-close {
  width: 28px; height: 28px;
  border-radius: 6px;
  display: flex; align-items: center; justify-content: center;
  cursor: pointer;
  color: var(--muted);
  font-size: 16px;
  transition: all var(--duration-fast);
  background: transparent;
  border: none;
  font-family: inherit;
}
.modal-close:hover { background: rgba(239,68,68,0.15); color: #ef4444; }

.modal-body { padding: 20px 24px; }

/* Tutorial content */
.tut-section { margin-bottom: 20px; }
.tut-section:last-child { margin-bottom: 0; }
.tut-heading {
  font-size: 12px; font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.06em;
  color: var(--accent);
  margin-bottom: 8px;
}
.tut-row {
  display: flex; gap: 12px;
  padding: 5px 0;
  font-size: 13px;
  color: rgba(255,255,255,0.7);
}
.tut-key {
  min-width: 160px;
  font-family: 'Cascadia Code', 'Consolas', monospace;
  font-size: 12px;
  color: var(--accent-cyan);
}
.tut-desc { color: rgba(255,255,255,0.6); }

/* Tips */
.tut-tip {
  padding: 8px 12px;
  background: rgba(99,102,241,0.06);
  border-left: 2px solid var(--accent);
  border-radius: 0 8px 8px 0;
  font-size: 12px;
  color: rgba(255,255,255,0.5);
  margin-top: 4px;
}

.modal-footer {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 16px 24px 20px;
  border-top: 0.5px solid var(--glass-border);
}

/* Checkbox */
.cbx-wrap {
  display: flex; align-items: center; gap: 8px;
  cursor: pointer;
  font-size: 13px; color: var(--muted);
}
.cbx-wrap input { display: none; }
.cbx-box {
  width: 18px; height: 18px;
  border-radius: 4px;
  border: 1.5px solid rgba(255,255,255,0.15);
  display: flex; align-items: center; justify-content: center;
  transition: all var(--duration-fast);
  flex-shrink: 0;
}
.cbx-wrap input:checked + .cbx-box {
  background: var(--accent);
  border-color: var(--accent);
}
.cbx-box::after {
  content: '';
  width: 6px; height: 10px;
  border: solid #fff;
  border-width: 0 2px 2px 0;
  transform: rotate(45deg) translateY(-1px);
  opacity: 0;
  transition: opacity var(--duration-fast);
}
.cbx-wrap input:checked + .cbx-box::after { opacity: 1; }

/* Modal buttons */
.btn-primary {
  padding: 10px 28px;
  background: var(--accent);
  border: none;
  border-radius: 10px;
  color: #fff;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  transition: all var(--duration-normal) var(--ease-smooth);
  font-family: inherit;
}
.btn-primary:hover {
  background: var(--accent-hover);
  box-shadow: 0 4px 20px rgba(99,102,241,0.35);
  transform: translateY(-1px);
}
.btn-primary:active { transform: scale(0.96); }

/* Tabs */
.modal-tabs {
  display: flex;
  flex-wrap: wrap;
  gap: 4px;
  margin-bottom: 16px;
}
.tab-btn {
  padding: 6px 14px;
  border-radius: 20px;
  font-size: 12px;
  font-weight: 500;
  color: var(--muted);
  cursor: pointer;
  transition: all var(--duration-fast) var(--ease-smooth);
  background: transparent;
  border: 0.5px solid transparent;
  font-family: inherit;
  white-space: nowrap;
}
.tab-btn:hover { color: var(--text); background: rgba(255,255,255,0.04); }
.tab-btn.active {
  background: rgba(99,102,241,0.12);
  border-color: rgba(99,102,241,0.3);
  color: #a5b4fc;
}
.tab-content {
  font-size: 13px;
  color: rgba(255,255,255,0.65);
  line-height: 2;
  white-space: pre-wrap;
}

/* Responsive */
@media (max-width: 700px) {
  .content { flex-direction: column; }
  .card-left { width: 100%; min-width: 0; }
}
</style>
</head>
<body>

<div id="app">
  <!-- TITLE BAR -->
  <div class="titlebar" id="titlebar">
    <div class="titlebar-logo">
      <span class="titlebar-dot"></span>
      Nexus Cast
      <span class="titlebar-ver">v3.3</span>
    </div>
    <div class="titlebar-actions">
      <button class="tb-btn" id="btnMin" title="最小化">--</button>
      <button class="tb-btn close" id="btnClose" title="关闭">x</button>
    </div>
  </div>

  <!-- MAIN CONTENT -->
  <div class="content">
    <!-- LEFT CARD -->
    <div class="card card-left">
      <div class="card-header">
        <div class="card-title">屏幕镜像</div>
        <div class="card-subtitle">将安卓手机画面投射到电脑</div>
      </div>

      <!-- Connection mode -->
      <div>
        <div class="sec-label">连接方式</div>
        <div class="segmented" id="modeGroup">
          <button class="seg-option active" data-idx="0">WiFi &nbsp;无线连接</button>
          <button class="seg-option" data-idx="1">USB &nbsp;数据线</button>
        </div>
      </div>

      <!-- IP -->
      <div>
        <div class="sec-label">IP 地址</div>
        <div class="ip-row">
          <input class="ip-input" id="ipBox" value="" placeholder="输入手机 IP 地址">
          <button class="ip-detect" id="btnDetect">检测</button>
        </div>
      </div>

      <!-- Quality -->
      <div>
        <div class="sec-label">画质</div>
        <select class="q-select" id="qualityBox">
          <option value="0">流畅 &nbsp;-- &nbsp;低延迟 &middot; 1280p &middot; 无音频</option>
          <option value="1">均衡 &nbsp;-- &nbsp;良好画质 &middot; 1920p &middot; 有音频</option>
          <option value="2">高清 &nbsp;-- &nbsp;最佳画质 &middot; 原生分辨率 &middot; 有音频</option>
        </select>
      </div>

      <!-- Connect -->
      <button class="btn-connect" id="btnConnect">连接</button>

      <!-- Status + link -->
      <div class="status-row">
        <span class="status-text" id="statusText"><span class="status-dot"></span>就绪</span>
        <button class="status-link" id="btnHelp">USB 调试指南</button>
      </div>
    </div>

    <!-- RIGHT CARD -->
    <div class="card card-right" id="rightCard">
      <div class="device-icon-wrap">
        <span class="device-icon">O</span>
      </div>
      <div class="device-name" id="deviceName">未连接设备</div>
      <div class="device-sub" id="deviceSub">连接手机后在此查看设备信息</div>
    </div>
  </div>
</div>

<!-- TUTORIAL MODAL -->
<div id="tutorialOverlay" style="display:none">
  <div class="modal-overlay">
    <div class="modal" style="width:500px">
      <div class="modal-header">
        <span class="modal-title">投屏使用教程</span>
        <button class="modal-close" id="tutClose">x</button>
      </div>
      <div class="modal-body">
        <div class="tut-section">
          <div class="tut-heading">鼠标操作</div>
          <div class="tut-row"><span class="tut-key">左键单击</span><span class="tut-desc">触摸点击屏幕</span></div>
          <div class="tut-row"><span class="tut-key">右键单击</span><span class="tut-desc">返回上一级</span></div>
          <div class="tut-row"><span class="tut-key">中键单击</span><span class="tut-desc">返回主屏幕</span></div>
          <div class="tut-row"><span class="tut-key">滚轮滑动</span><span class="tut-desc">滚动 / 滑动页面</span></div>
          <div class="tut-row"><span class="tut-key">拖拽</span><span class="tut-desc">滑动 / 长按</span></div>
        </div>
        <div class="tut-section">
          <div class="tut-heading">键盘快捷键</div>
          <div class="tut-row"><span class="tut-key">Ctrl + O</span><span class="tut-desc">熄屏 / 亮屏</span></div>
          <div class="tut-row"><span class="tut-key">Ctrl + Shift + N</span><span class="tut-desc">展开快捷设置面板</span></div>
          <div class="tut-row"><span class="tut-key">Ctrl + N</span><span class="tut-desc">通知面板</span></div>
          <div class="tut-row"><span class="tut-key">Ctrl + S</span><span class="tut-desc">切换应用</span></div>
          <div class="tut-row"><span class="tut-key">Ctrl + F</span><span class="tut-desc">全屏</span></div>
          <div class="tut-row"><span class="tut-key">Ctrl + C / V</span><span class="tut-desc">复制 / 粘贴</span></div>
        </div>
        <div class="tut-tip">* 手机锁屏状态也能投屏操控 &nbsp;| &nbsp;* 流畅模式延迟最低 &nbsp;| &nbsp;* 建议使用 5GHz WiFi &nbsp;| &nbsp;* USB 连接后可切换 WiFi 无线 &nbsp;| &nbsp;* 关闭镜像窗口即断开</div>
      </div>
      <div class="modal-footer">
        <label class="cbx-wrap" id="cbxSkip">
          <input type="checkbox" id="skipCheck">
          <span class="cbx-box"></span>
          不再显示
        </label>
        <button class="btn-primary" id="tutStart">开始投屏</button>
      </div>
    </div>
  </div>
</div>

<!-- HELP MODAL -->
<div id="helpOverlay" style="display:none">
  <div class="modal-overlay">
    <div class="modal" style="width:540px">
      <div class="modal-header">
        <span class="modal-title">USB 调试指南</span>
        <button class="modal-close" id="helpClose">x</button>
      </div>
      <div class="modal-body">
        <div class="modal-tabs" id="helpTabs">
          <button class="tab-btn active" data-tab="0">Xiaomi / Redmi</button>
          <button class="tab-btn" data-tab="1">Huawei / Honor</button>
          <button class="tab-btn" data-tab="2">Samsung</button>
          <button class="tab-btn" data-tab="3">OPPO / OnePlus</button>
          <button class="tab-btn" data-tab="4">vivo / iQOO</button>
          <button class="tab-btn" data-tab="5">通用</button>
        </div>
        <div class="tab-content" id="helpContent"></div>
      </div>
      <div class="modal-footer" style="justify-content:flex-end">
        <button class="btn-primary" id="helpOk">知道了</button>
      </div>
    </div>
  </div>
</div>

<script>
// ═══════════════════════════════════════════════════════════════════
// Nexus Cast — Main Application
// ═══════════════════════════════════════════════════════════════════

// --- WebView2 bridge detection ---
const isWebView = (typeof chrome !== 'undefined' && chrome.webview);
let bridgeReady = false;

// --- DOM refs ---
const $ = (s) => document.querySelector(s);
const $$ = (s) => document.querySelectorAll(s);

const modeBtns = $$('#modeGroup button');
const ipBox = $('#ipBox');
const btnDetect = $('#btnDetect');
const qualityBox = $('#qualityBox');
const btnConnect = $('#btnConnect');
const statusText = $('#statusText');
const deviceNameEl = $('#deviceName');
const deviceSubEl = $('#deviceSub');
const tutorialOverlay = $('#tutorialOverlay');
const helpOverlay = $('#helpOverlay');
const helpContent = $('#helpContent');

let mode = 0; // 0=wifi, 1=usb
let connecting = false;
let skipTutorial = false;

// --- Bridge helpers ---
async function bridgeCall(method, arg) {
  try {
    var bridge = chrome.webview.hostObjects.bridge;
    return await bridge.Call(method, arg || '');
  } catch(e) {
    return '';
  }
}

// Direct API functions (bypass HTTP, call C# bridge directly)
async function adb(cmd)          { return await bridgeCall('adb', cmd); }
async function checkUsb()        { var r = await bridgeCall('check-usb', ''); return r === 'true'; }
async function detectIp()        { return await bridgeCall('detect-ip', ''); }
async function deviceName()      { return await bridgeCall('device-name', ''); }
async function usbSerial()       { return await bridgeCall('usb-serial', ''); }
async function tcpip()           { await bridgeCall('tcpip', ''); }
async function skipTutorialCheck(){ var r = await bridgeCall('skip-tutorial', ''); return r === 'true'; }
async function skipTutorialSave() { await bridgeCall('set-skip-tutorial', ''); }
async function killAdb()         { await bridgeCall('kill-server', ''); }
async function launchApp(flag, name, quality) {
  await bridgeCall('launch', JSON.stringify({ flag: flag, name: name, quality: quality }));
}
async function minimizeApp()     { await bridgeCall('minimize', ''); }
async function closeApp()        { await bridgeCall('close', ''); }

function setStatus(msg, type) {
  const dot = statusText.querySelector('.status-dot');
  dot.className = 'status-dot ' + (type || '');
  statusText.childNodes[1] && (statusText.childNodes[1].textContent = msg);
  statusText.childNodes[0] = dot;
}

// --- Connection mode ---
modeBtns.forEach(b => {
  b.addEventListener('click', () => {
    modeBtns.forEach(x => x.classList.remove('active'));
    b.classList.add('active');
    mode = parseInt(b.dataset.idx);
    ipBox.disabled = mode === 1;
    btnDetect.disabled = mode === 1;
  });
});

// --- Detect IP ---
btnDetect.addEventListener('click', async () => {
  btnDetect.disabled = true;
  setStatus('正在通过 USB 检测手机 IP...', 'busy');
  const ip = await detectIp();
  if (ip && ip.length > 0) {
    ipBox.value = ip;
    setStatus('已检测到 IP: ' + ip, 'ok');
    deviceNameEl.textContent = '已发现设备';
  } else {
    setStatus('无法检测 IP，请检查 USB 连接', 'err');
  }
  btnDetect.disabled = false;
});

// --- Help tabs ---
const helpData = [
  "1. 设置 > 关于手机\n2. 连续点击 [MIUI 版本] (HyperOS: [OS 版本]) 7 次\n3. 你现在已经是开发者了！\n4. 设置 > 更多设置 > 开发者选项\n5. 开启: USB 调试、USB 安装、\n   USB 调试（安全设置）\n\n注意: HyperOS 可能将这些选项放在子菜单中",
  "1. 设置 > 关于手机\n2. 连续点击 [版本号] 7 次\n3. 设置 > 系统和更新 > 开发人员选项\n4. 开启 [USB 调试]\n5. 点击 [确定] 确认\n\nHarmonyOS: 设置 > 系统 > 开发者选项",
  "1. 设置 > 关于手机 > 软件信息\n2. 连续点击 [版本号] 7 次\n3. 设置 > 开发者选项 (位于底部)\n4. 开启 [USB 调试]",
  "1. 设置 > 关于本机 > 版本信息\n2. 连续点击 [版本号] 7 次\n3. 设置 > 其他设置 > 开发者选项\n4. 开启 [USB 调试]\n5. 同时开启 [禁止权限监控]\n\nColorOS 15: 设置 > 关于 > 版本信息",
  "1. 设置 > 系统 > 关于手机\n2. 连续点击 [软件版本号] 7 次\n3. 设置 > 系统 > 开发者选项\n4. 开启 [USB 调试]\n\n部分机型: 拨号 *#*#7777#*#*",
  "1. 打开设置，搜索 [版本号]\n2. 连续快速点击 7 次\n3. 搜索 [开发者选项]\n4. 开启 [USB 调试]\n\n连接 USB 后:\n- 在手机弹窗上点击 [允许]\n- 勾选 [一律允许]\n\n还是不行？切换 USB 模式为 [文件传输]"
];

$$('#helpTabs .tab-btn').forEach(b => {
  b.addEventListener('click', () => {
    $$('#helpTabs .tab-btn').forEach(x => x.classList.remove('active'));
    b.classList.add('active');
    helpContent.textContent = helpData[parseInt(b.dataset.tab)];
  });
});
helpContent.textContent = helpData[0];

// --- Connect ---
btnConnect.addEventListener('click', async () => {
  if (connecting) return;
  connecting = true;
  btnConnect.disabled = true;
  btnConnect.textContent = '连接中...';

  const wifi = mode === 0;
  const quality = parseInt(qualityBox.value);
  const ip = ipBox.value.trim();

  // Kill server
  await killAdb();
  await sleep(500);

  let flag, name;

  if (wifi) {
    if (!ip) {
      setStatus('请输入手机 IP 地址或用 [检测] 按钮自动获取', 'err');
      resetConnect(); return;
    }
    setStatus('正在连接 ' + ip + ' ...', 'busy');

    const cr = await adb('connect ' + ip + ':5555');
    if (cr && !cr.includes('cannot') && !cr.includes('failed')) {
      flag = '-s ' + ip + ':5555';
    } else {
      // Try USB-assisted
      const hasUsb = await checkUsb();
      if (hasUsb) {
        setStatus('正在通过 USB 开启无线调试...', 'busy');
        await tcpip();
        await sleep(1500);
        setStatus('正在切换到 WiFi 连接...', 'busy');
        const cr2 = await adb('connect ' + ip + ':5555');
        if (cr2 && !cr2.includes('cannot') && !cr2.includes('failed')) {
          flag = '-s ' + ip + ':5555';
        } else {
          setStatus('WiFi 连接失败，请确认在同一网络', 'err');
          resetConnect(); return;
        }
      } else {
        setStatus('WiFi 失败，请插入 USB 或开启无线调试', 'err');
        resetConnect(); return;
      }
    }
    name = await deviceName();
    if (!name) name = 'Android Device';
  } else {
    setStatus('正在检查 USB 连接...', 'busy');
    const hasUsb = await checkUsb();
    if (!hasUsb) {
      setStatus('未检测到 USB 设备，请检查数据线和 USB 调试开关', 'err');
      resetConnect(); return;
    }
    var serial = await usbSerial(); if (!serial) { setStatus('serial err','err'); resetConnect(); return; } flag = '-s ' + serial
    name = await deviceName();
    if (!name) name = 'Android Device';
  }

  // Check tutorial
  const skip = await skipTutorialCheck();
  if (!skip) {
    connecting = false;
    btnConnect.disabled = false;
    btnConnect.textContent = '连接';
    showTutorial(flag, name, quality);
    return;
  }

  await doLaunch(flag, name, quality);
});

async function doLaunch(flag, name, quality) {
  connecting = true;
  btnConnect.disabled = true;
  btnConnect.textContent = '连接中...';

  setStatus('正在启动...', 'busy');
  await launchApp(flag, name, quality);
  setStatus('运行中  |  ' + name + '  |  关闭镜像窗口即断开连接', 'ok');
  deviceNameEl.textContent = name;
  deviceSubEl.textContent = '投屏已启动';
  resetConnect();
}

function resetConnect() {
  connecting = false;
  btnConnect.disabled = false;
  btnConnect.textContent = '连接';
}

function sleep(ms) { return new Promise(r => setTimeout(r, ms)); }

// --- Tutorial modal ---
function showTutorial(flag, name, quality) {
  tutorialOverlay.style.display = 'block';
  $('#tutStart').onclick = async () => {
    if ($('#skipCheck').checked) {
      await skipTutorialSave();
    }
    tutorialOverlay.style.display = 'none';
    await doLaunch(flag, name, quality);
  };
  $('#tutClose').onclick = () => {
    tutorialOverlay.style.display = 'none';
    setStatus('已取消', '');
    resetConnect();
  };
}

// Close modals on overlay click
tutorialOverlay.addEventListener('click', (e) => {
  if (e.target === tutorialOverlay || e.target.classList.contains('modal-overlay')) {
    tutorialOverlay.style.display = 'none';
    setStatus('已取消', '');
    resetConnect();
  }
});
helpOverlay.addEventListener('click', (e) => {
  if (e.target === helpOverlay || e.target.classList.contains('modal-overlay')) {
    helpOverlay.style.display = 'none';
  }
});

// --- Help modal ---
$('#btnHelp').addEventListener('click', () => {
  helpOverlay.style.display = 'block';
});
$('#helpClose').addEventListener('click', () => {
  helpOverlay.style.display = 'none';
});
$('#helpOk').addEventListener('click', () => {
  helpOverlay.style.display = 'none';
});

// --- Title bar actions ---
$('#btnMin').addEventListener('click', () => minimizeApp());
$('#btnClose').addEventListener('click', () => closeApp());

// --- Title bar drag (native, zero-lag) ---
$('#titlebar').addEventListener('mousedown', function(e) {
  if (e.target.classList.contains('tb-btn')) return;
  bridgeCall('begin-drag', '');
});

// --- Keyboard shortcut to close modals ---
document.addEventListener('keydown', (e) => {
  if (e.key === 'Escape') {
    if (tutorialOverlay.style.display === 'block') {
      tutorialOverlay.style.display = 'none';
      setStatus('已取消', '');
      resetConnect();
    }
    if (helpOverlay.style.display === 'block') {
      helpOverlay.style.display = 'none';
    }
  }
});

// Initial status check
(async () => {
  const hasUsb = await checkUsb();
  if (hasUsb) {
    setStatus('已检测到 USB 设备', 'ok');
    deviceNameEl.textContent = '已发现设备';
    deviceSubEl.textContent = '可选择 USB 或 WiFi 模式连接';
  }
})();
</script>
</body>
</html>