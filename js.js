const canvas = document.getElementById('gameCanvas');
const ctx = canvas.getContext('2d');
const scoreEl = document.getElementById('scoreValue');

let snake;
let food;
let score;
let interval;

// Устанавливаем начальные значения
function init() {
    canvas.width = 500;
    canvas.height = 500;
    
    snake = [{x: 250, y: 250}];
    food = getRandomPosition();
    score = 0;
    updateScore();

    draw();
}

// Генерирует случайную позицию для еды
function getRandomPosition() {
    let x = Math.floor(Math.random() * canvas.width / 10) * 10;
    let y = Math.floor(Math.random() * canvas.height / 10) * 10;
    return {x, y};
}

// Обновляет значение счёта
function updateScore() {
    scoreEl.textContent = score;
}

// Рисуем все объекты на канвасе
function draw() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Рисуем змею
    snake.forEach(part => {
        ctx.fillStyle = 'green';
        ctx.fillRect(part.x, part.y, 10, 10);
    });

    // Рисуем еду
    ctx.fillStyle = 'red';
    ctx.fillRect(food.x, food.y, 10, 10);
}

// Проверяет, съела ли змея еду
function checkEatFood() {
    const head = snake[snake.length - 1];
    if (head.x === food.x && head.y === food.y) {
        score++;
        updateScore();
        food = getRandomPosition();
        return true;
    }
    return false;
}

// Двигает змею вперед
function move(direction) {
    const head = {...snake[snake.length - 1]};

    switch (direction) {
        case 'up':
            head.y -= 10;
            break;
        case 'down':
            head.y += 10;
            break;
        case 'left':
            head.x -= 10;
            break;
        case 'right':
            head.x += 10;
            break;
    }

    if (checkCollision(head)) {
        clearInterval(interval);
        alert(`Игра окончена! Ваш счёт: ${score}`);
        return;
    }

    if (!checkEatFood()) {
        snake.shift();
    } else {
        score++;
        updateScore();
    }

    snake.push(head);
    draw();
}

// Проверяет, не столкнулась ли змея со стеной или самой собой
function checkCollision(head) {
    if (
        head.x < 0 || head.x >= canvas.width ||
        head.y < 0 || head.y >= canvas.height ||
        snake.some(part => part.x === head.x && part.y === head.y)
    ) {
        return true;
    }
    return false;
}

// Начинает игру
function startGame() {
    init();
    document.removeEventListener('keydown', onKeyDown);
    document.addEventListener('keydown', onKeyDown);
    interval = setInterval(() => move('right'), 100);
}

// Обрабатывает нажатия клавиш
function onKeyDown(event) {
    switch (event.key) {
        case 'ArrowUp':
            clearInterval(interval);
            interval = setInterval(() => move('up'), 100);
            break;
        case 'ArrowDown':
            clearInterval(interval);
            interval = setInterval(() => move('down'), 100);
            break;
        case 'ArrowLeft':
            clearInterval(interval);
            interval = setInterval(() => move('left'), 100);
            break;
        case 'ArrowRight':
            clearInterval(interval);
            interval = setInterval(() => move('right'), 100);
            break;
    }
}

init();
