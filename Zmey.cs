import pygame
import random

# Инициализация Pygame
pygame.init()

# Размеры окна
screen_width = 600
screen_height = 400

# Настройки цвета
white = (255, 255, 255)
black = (0, 0, 0)
red = (255, 0, 0)
green = (0, 155, 0)

# Создаем окно
gameDisplay = pygame.display.set_mode((screen_width, screen_height))
pygame.display.set_caption('Змейка')

clock = pygame.time.Clock()

block_size = 10
FPS = 15

font_style = pygame.font.SysFont("bahnschrift", 25)
score_font = pygame.font.SysFont("comicsansms", 35)

def message(msg, color):
    mesg = font_style.render(msg, True, color)
    gameDisplay.blit(mesg, [screen_width / 6, screen_height / 3])

def your_score(score):
    value = score_font.render("Ваш счет: " + str(score), True, green)
    gameDisplay.blit(value, [0, 0])

def our_snake(block_size, snake_list):
    for x in snake_list:
        pygame.draw.rect(gameDisplay, black, [x[0], x[1], block_size, block_size])

def gameLoop():
    game_over = False
    game_close = False

    x1 = screen_width / 2
    y1 = screen_height / 2

    x1_change = 0
    y1_change = 0

    snake_List = []
    Length_of_snake = 1

    foodx = round(random.randrange(0, screen_width - block_size) / 10.0) * 10.0
    foody = round(random.randrange(0, screen_height - block_size) / 10.0) * 10.0

    while not game_over:

        while game_close == True:
            gameDisplay.fill(white)
            message("Вы проиграли! Нажмите Q-Quit или C-Play Again", red)
            your_score(Length_of_snake - 1)
            pygame.display.update()

            for event in pygame.event.get():
                if event.type == pygame.KEYDOWN:
                    if event.key == pygame.K_q:
                        game_over = True
                        game_close = False
                    if event.key == pygame.K_c:
                        gameLoop()

        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                game_over = True
            if event.type == pygame.KEYDOWN:
                if event.key == pygame.K_LEFT:
                    x1_change = -block_size
                    y1_change = 0
                elif event.key == pygame.K_RIGHT:
                    x1_change = block_size
                    y1_change = 0
                elif event.key == pygame.K_UP:
                    y1_change = -block_size
                    x1_change = 0
                elif event.key == pygame.K_DOWN:
                    y1_change = block_size
                    x1_change = 0

        if x1 >= screen_width or x1 < 0 or y1 >= screen_height or y1 < 0:
            game_close = True
        
        x1 += x1_change
        y1 += y1_change
        gameDisplay.fill(white)
        pygame.draw.rect(gameDisplay, green, [foodx, foody, block_size, block_size])
        snake_Head = []
        snake_Head.append(x1)
        snake_Head.append(y1)
        snake_List.append(snake_Head)
        if len(snake_List) > Length_of_snake:
            del snake_List[0]

        for each_segment in snake_List[:-1]:
            if each_segment == snake_Head:
                game_close = True

        our_snake(block_size, snake_List)
        your_score(Length_of_snake - 1)

        pygame.display.update()

        if x1 == foodx and y1 == foody:
            foodx = round(random.randrange(0, screen_width - block_size) / 10.0) * 10.0
            foody = round(random.randrange(0, screen_height - block_size) / 10.0) * 10.0
            Length_of_snake += 1

        clock.tick(FPS)

    pygame.quit()
    quit()

gameLoop()
